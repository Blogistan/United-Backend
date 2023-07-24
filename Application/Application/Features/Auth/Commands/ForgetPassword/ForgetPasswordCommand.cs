using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Mailing;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using MimeKit;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace Application.Features.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string PasswordResetUrl { get; set; }
        public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Unit>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMailService mailService;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IForgotPasswordRepository forgotPasswordRepository;
            public ForgetPasswordCommandHandler(ISiteUserRepository siteUserRepository, IMailService mailService, AuthBussinessRules authBussinessRules, IForgotPasswordRepository forgotPasswordRepository)
            {
                this.siteUserRepository = siteUserRepository;
                this.mailService = mailService;
                this.authBussinessRules = authBussinessRules;
                this.forgotPasswordRepository = forgotPasswordRepository;
            }

            public async Task<Unit> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await siteUserRepository.GetAsync(x => x.Email == request.Email);
                await authBussinessRules.UserShouldBeExist(user);

                var key = await CreateResetKey();
                await forgotPasswordRepository.AddAsync(new ForgotPassword
                {
                    ActivationKey=key,
                    ExpireDate=DateTime.UtcNow.AddMinutes(15),
                    IsVerified=false,
                    UserId=user.Id,
                });

                await SendForgotPasswordMail(user, request.PasswordResetUrl,key);

                return Unit.Value;
            }
            private async Task SendForgotPasswordMail(SiteUser siteUser, string passwordResetUrl,string resetKey)
            {
                List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>();
                mailboxAddresses.Add(new MailboxAddress(Encoding.UTF8, $"{siteUser.FirstName} {siteUser.LastName}", siteUser.Email));

                Mail mail = new()
                {
                    Subject = "Forgot Password",
                    ToList = mailboxAddresses,
                    HtmlBody = $"Hi {siteUser.FirstName} {siteUser.LastName} " +
                    $"Here is your password reset link " +
                    $"{passwordResetUrl}?resetKey={resetKey}"
                };

                await mailService.SendEmailAsync(mail);
            }
            private async Task<string> CreateResetKey()
            {
                 return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            }
        }
    }
}
