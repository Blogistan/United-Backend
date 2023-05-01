using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand
{
    public class VerifyEmailAuthenticatorCommand : IRequest
    {
        public string ActivationKey { get; set; }
        public string[] Roles => Array.Empty<string>();

        public class VerifyEmailAuthenticatorCommandHandler : IRequestHandler<VerifyEmailAuthenticatorCommand>
        {
            private readonly IEmailAuthenticatorRepository emailAuthenticatorRepository;
            private readonly AuthBussinessRules authBussinessRules;
            public VerifyEmailAuthenticatorCommandHandler(IEmailAuthenticatorRepository emailAuthenticatorRepository, AuthBussinessRules authBussinessRules)
            {
                this.emailAuthenticatorRepository = emailAuthenticatorRepository;
                this.authBussinessRules = authBussinessRules;
            }
            public async Task Handle(VerifyEmailAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.ActivationKey == request.ActivationKey, include: x => x.Include(x => x.User));


                await authBussinessRules.UserEmailAuthenticatorShouldBeExists(emailAuthenticator);

                await VerifyEmailAuthenticator(emailAuthenticator);
                return Task.CompletedTask;
            }

            private async Task VerifyEmailAuthenticator(EmailAuthenticator emailAuthenticator)
            {
                emailAuthenticator.IsVerified = true;
                emailAuthenticator.ActivationKey = null;
                emailAuthenticator.User.AuthenticatorType = AuthenticatorType.Email;
                await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
            }
        }
    }
}
