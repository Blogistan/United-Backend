using Application.Features.Auth.Rules;
using Application.Notifications.PasswordChangedNotification;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetCommand : IRequest<Unit>
    {
        public string ResetKey { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
        public string NewPasswordConfirm { get; set; } = string.Empty;

        public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Unit>
        {
            private readonly IAuthService authService;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IForgotPasswordRepository forgotPasswordRepository;
            private readonly IMediator mediator;


            public PasswordResetCommandHandler(IAuthService authService, ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, IForgotPasswordRepository forgotPasswordRepository, IMailService mailService, IMediator mediator)
            {
                this.authService = authService;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.forgotPasswordRepository = forgotPasswordRepository;
                this.mediator = mediator;
            }

            public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
            {
                ForgotPassword forgotPassword = await forgotPasswordRepository.GetAsync(x => x.ActivationKey == request.ResetKey);
                SiteUser siteUser = await siteUserRepository.GetAsync(x => x.Id == forgotPassword.UserId);
                await authBussinessRules.PasswordResetKeyShouldBeExists(forgotPassword);

                await authBussinessRules.PasswordResetTokenShouldBeActive(forgotPassword);

                byte[] hash, salt;

                HashingHelper.CreatePasswordHash(request.NewPassword, out hash, out salt);

                forgotPassword.NewPasswordSalt = salt;
                forgotPassword.NewPasswordHash = hash;
                forgotPassword.OldPasswordSalt = siteUser.PasswordSalt;
                forgotPassword.OldPasswordHash = siteUser.PasswordHash;
                forgotPassword.IsVerified = true;

                siteUser.PasswordSalt = salt;
                siteUser.PasswordHash = hash;

                await forgotPasswordRepository.UpdateAsync(forgotPassword);
                await siteUserRepository.UpdateAsync(siteUser);

                await mediator.Publish(new PasswordChangedNotification() { User = siteUser });

                return Unit.Value;


            }


        }
    }
}
