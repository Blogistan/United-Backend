using FluentValidation;

namespace Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand
{
    public class VerifyEmailAuthenticatorCommandValidator : AbstractValidator<VerifyEmailAuthenticatorCommand>
    {
        public VerifyEmailAuthenticatorCommandValidator()
        {
            RuleFor(x => x.ActivationKey).NotEmpty();
        }
    }
}
