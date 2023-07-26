using Core.Mailing;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Text;

namespace Application.Notifications.PasswordChangedNotification
{
    public class PasswordChangedNotificationHandler : INotificationHandler<PasswordChangedNotification>
    {
        private readonly IMailService mailService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PasswordChangedNotificationHandler(IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            this.mailService = mailService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task Handle(PasswordChangedNotification notification, CancellationToken cancellationToken)
        {
            await SendPasswordChangeMail(notification.User);
        }
        private async Task SendPasswordChangeMail(User siteUser)
        {
            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>();
            mailboxAddresses.Add(new MailboxAddress(Encoding.UTF8, $"{siteUser.FirstName} {siteUser.LastName}", siteUser.Email));

            var info = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            Mail mail = new()
            {
                Subject = "Password Changed",
                ToList = mailboxAddresses,
                HtmlBody = $"Hi {siteUser.FirstName} {siteUser.LastName} \n" +
                $"Your password is changed  at, \n" +
                $"{info}"
            };

            await mailService.SendEmailAsync(mail);

        }
    }
}
