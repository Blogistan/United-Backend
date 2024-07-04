using Core.Mailing;
using Core.Security.Entities;
using Infrastructure.IpStack.Abstract;
using Infrastructure.IpStack.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Text;
using UAParser;


namespace Application.Notifications.PasswordChangedNotification
{
    public class PasswordChangedNotificationHandler : INotificationHandler<PasswordChangedNotification>
    {
        private readonly IMailService mailService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IIpStackService ipStackService;
        public PasswordChangedNotificationHandler(IMailService mailService, IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IIpStackService ipStackService)
        {
            this.mailService = mailService;
            this.httpContextAccessor = httpContextAccessor;
            this.ipStackService = ipStackService;
        }
        public async Task Handle(PasswordChangedNotification notification, CancellationToken cancellationToken)
        {
            await SendPasswordChangeMail(notification.User);
        }
        private async Task SendPasswordChangeMail(User siteUser)
        {
            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>();
            mailboxAddresses.Add(new MailboxAddress(Encoding.UTF8, $"{siteUser.FirstName} {siteUser.LastName}", siteUser.Email));

            var request = httpContextAccessor.HttpContext.Request;
            var uaParser = Parser.GetDefault();

            ClientInfo client = uaParser.Parse(request.Headers["User-Agent"]);
            var browser = client.ToString();


            var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();


            IPInfo ipInfo = await ipStackService.GetClientIpInfo(ipAddress);

            Mail mail = new()
            {
                Subject = "Password Changed",
                ToList = mailboxAddresses,
                HtmlBody = mailService.LoadMailTemplate("D:\\Workstation\\mvc\\LastDance\\C\\United\\United-Backend\\corePackages\\Core.Mailing\\Core.Mailing\\MailDesigns\\PasswordChanced\\PasswordChanced.html").Replace("FIRST_NAME", siteUser.FirstName).Replace("IP_ADDRESS", ipAddress).Replace("WEB_BROWSER", browser).Replace("IP_LOCATION", $"{ipInfo.Country_Name} {ipInfo.Region_Name}")
            };

            await mailService.SendEmailAsync(mail);

        }
    }
}
