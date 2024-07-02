using Application.Features.Auth.Constants;
using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Mailing;
using Core.Security.Entities;
using MediatR;
using MimeKit;
using System.Text;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator
{
    public class EnableEmailAuthenticatorCommand : IRequest, ISecuredRequest
    {
        public int UserID { get; set; }
        public string VerifyToEmail { get; set; }

        public string[] Roles => new string[] { "User" };

        public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
        {
            private readonly IAuthService authService;
            private readonly IEmailAuthenticatorRepository emailAuthenticatorRepository;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IMailService mailService;


            public EnableEmailAuthenticatorCommandHandler(IAuthService authService, IMailService mailService
                , IEmailAuthenticatorRepository emailAuthenticatorRepository, ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules)
            {
                this.authBussinessRules = authBussinessRules;
                this.emailAuthenticatorRepository = emailAuthenticatorRepository;
                this.authService = authService;
                this.siteUserRepository = siteUserRepository;
                this.mailService = mailService;
            }

            public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                UserBase? siteUser = await siteUserRepository.GetAsync(x => x.Id == request.UserID && x.IsActive == true);

                await authBussinessRules.UserShouldBeExist(siteUser);

                await authBussinessRules.UserShouldNotBeHasAuthenticator(siteUser);

                await emailAuthenticatorRepository.DeleteAllNonVerifiedAsync(siteUser);

                EmailAuthenticator emailAuthenticator = await authService.CreateEmailAutenticator(siteUser);

                await emailAuthenticatorRepository.AddAsync(emailAuthenticator);

                await SendInfoMailAsync(siteUser, emailAuthenticator.ActivationKey, request.VerifyToEmail);


            }
            public async Task SendInfoMailAsync(UserBase siteUser, string activationKey, string VerifyToMail)
            {
                List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>();
                mailboxAddresses.Add(new MailboxAddress(Encoding.UTF8, $"{siteUser.FirstName} {siteUser.LastName}", siteUser.Email));

                Mail mailData = new()
                {
                    ToList = mailboxAddresses,
                    Subject = AuthBusinessMessage.VerifyEmail,
                    TextBody = $"{AuthBusinessMessage.ClickOnBelowLinkToVerifyEmail}\n" +
                           $"{VerifyToMail}?activationKey={HttpUtility.UrlEncode(activationKey)}"
                };
                await mailService.SendEmailAsync(mailData);
            }
        }
    }
}
