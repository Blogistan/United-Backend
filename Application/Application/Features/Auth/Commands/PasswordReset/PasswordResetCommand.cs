using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Text;

namespace Application.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetCommand : IRequest<Unit>
    {
        public string ResetKey { get; set; }

        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }

        public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Unit>
        {
            private readonly IAuthService authService;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IForgotPasswordRepository forgotPasswordRepository;
            private readonly IMailService mailService;
            private readonly IHttpContextAccessor httpContextAccessor;

            public PasswordResetCommandHandler(IAuthService authService, ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, IForgotPasswordRepository forgotPasswordRepository, IMailService mailService, IHttpContextAccessor httpContextAccessor)
            {
                this.authService = authService;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.forgotPasswordRepository = forgotPasswordRepository;
                this.mailService = mailService;
                this.httpContextAccessor = httpContextAccessor;
            }

            public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
            {
                ForgotPassword forgotPassword = await forgotPasswordRepository.GetAsync(x => x.ActivationKey == request.ResetKey, x => x.Include(x => x.User));

                await authBussinessRules.PasswordResetKeyShouldBeExists(forgotPassword);

                await authBussinessRules.PasswordResetTokenShouldBeActive(forgotPassword);

                byte[] hash, salt;

                HashingHelper.CreatePasswordHash(request.NewPassword, out hash, out salt);

                forgotPassword.NewPasswordSalt = salt;
                forgotPassword.NewPasswordHash = hash;
                forgotPassword.OldPasswordSalt = forgotPassword.User.PasswordSalt;
                forgotPassword.OldPasswordHash = forgotPassword.User.PasswordHash;
                forgotPassword.IsVerified = true;

                forgotPassword.User.PasswordSalt = salt;
                forgotPassword.User.PasswordHash = hash;

                await forgotPasswordRepository.UpdateAsync(forgotPassword);

                await SendPasswordChangeMail(forgotPassword.User);

                return Unit.Value;


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
}
