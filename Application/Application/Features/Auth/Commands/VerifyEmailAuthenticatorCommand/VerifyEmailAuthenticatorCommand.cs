using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand
{
    public class VerifyEmailAuthenticatorCommand : IRequest<Unit>
    {
        public string ActivationKey { get; set; } = string.Empty;

        public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand,Unit>
        {
            private readonly IEmailAuthenticatorRepository emailAuthenticatorRepository;
            private readonly AuthBussinessRules authBussinessRules;
            public VerifyEmailAuthenticatorCommandHandler(IEmailAuthenticatorRepository emailAuthenticatorRepository, AuthBussinessRules authBussinessRules)
            {
                this.emailAuthenticatorRepository = emailAuthenticatorRepository;
                this.authBussinessRules = authBussinessRules;
            }
            public async Task<Unit> Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.ActivationKey == request.ActivationKey, include: x => x.Include(x => x.User));


                await authBussinessRules.UserEmailAuthenticatorShouldBeExists(emailAuthenticator);

                await VerifyEmailAuthenticator(emailAuthenticator);
                return Unit.Value;
            }

            private async Task VerifyEmailAuthenticator(EmailAuthenticator emailAuthenticator)
            {
                emailAuthenticator.IsVerified = true;
                emailAuthenticator.User.AuthenticatorType = AuthenticatorType.Email;
                await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
            }
        }
    }
}
