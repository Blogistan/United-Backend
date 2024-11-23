using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using MimeKit;
using MimeKit.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

namespace Core.Mailing.MailKitImplementations;

public class MailKitMailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private DkimSigner? _signer;

    public MailKitMailService(IConfiguration configuration)
    {
        _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        _signer = null;
    }

    public void SendMail(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;

        EmailPrepare(mail, out MimeMessage email, out SmtpClient smtp);
        smtp.Send(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    public async Task SendEmailAsync(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;

        EmailPrepare(mail, out MimeMessage email, out SmtpClient smtp);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }
    //private async Task<string> GetAccessTokenAsync()
    //{
    //    var app = ConfidentialClientApplicationBuilder.Create(_mailSettings.ClientId)
    //        .WithClientSecret(_mailSettings.ClientSecret)
    //        .WithAuthority($"https://login.microsoftonline.com/{_mailSettings.TenatId}")
    //        .Build();

    //    var result = await app.AcquireTokenForClient(new[] { "https://outlook.office365.com/.default" }).ExecuteAsync();
    //    return result.AccessToken;
    //}

    private void EmailPrepare(Mail mail, out MimeMessage email, out SmtpClient smtp)
    {
        email = new MimeMessage();
        email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));
        email.To.AddRange(mail.ToList);
        if (mail.CcList != null && mail.CcList.Any())
            email.Cc.AddRange(mail.CcList);
        if (mail.BccList != null && mail.BccList.Any())
            email.Bcc.AddRange(mail.BccList);

        email.Subject = mail.Subject;
        BodyBuilder bodyBuilder = new() { TextBody = mail.TextBody, HtmlBody = mail.HtmlBody };

        if (mail.Attachments != null)
            foreach (var attachment in mail.Attachments)
                if (attachment != null)
                    bodyBuilder.Attachments.Add(attachment);

        email.Body = bodyBuilder.ToMessageBody();

        smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.AppPassword);  // Uygulama şifresi ile giriş
    }

    private AsymmetricKeyParameter ReadPrivateKeyFromPemEncodedString()
    {
        AsymmetricKeyParameter result;
        string pemEncodedKey = "-----BEGIN RSA PRIVATE KEY-----\n" + _mailSettings.DkimPrivateKey + "\n-----END RSA PRIVATE KEY-----";
        using (StringReader stringReader = new(pemEncodedKey))
        {
            PemReader pemReader = new(stringReader);
            object? pemObject = pemReader.ReadObject();
            result = ((AsymmetricCipherKeyPair)pemObject).Private;
        }

        return result;
    }

    public string LoadMailTemplate(string mailPath)
    {
        return File.ReadAllText(mailPath);
    }
}
