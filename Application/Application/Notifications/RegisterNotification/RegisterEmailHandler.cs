using Core.Mailing;
using Core.Security.Entities;
using MediatR;
using MimeKit;
using System.Text;

namespace Application.Notifications.RegisterNotification
{
    public class RegisterEmailHandler : INotificationHandler<RegisteredNotification>
    {
        private readonly IMailService mailService;

        public RegisterEmailHandler(IMailService mailService)
        {
            this.mailService = mailService;
        }

        public async Task Handle(RegisteredNotification notification, CancellationToken cancellationToken)
        {
            await SendInfoMailAsync(notification.SiteUser);
        }
        public async Task SendInfoMailAsync(User siteUser)
        {
            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>();
            mailboxAddresses.Add(new MailboxAddress(Encoding.UTF8, $"{siteUser.FirstName} {siteUser.LastName}", siteUser.Email));

            Mail mailData = new()
            {
                ToList = mailboxAddresses,
                Subject = "Welcome To United !",
                TextBody = $"Welcome to United {siteUser.FirstName} {siteUser.LastName} , We hope you learn and share new information , United team ."
            };
            await mailService.SendEmailAsync(mailData);
        }
    }
}
