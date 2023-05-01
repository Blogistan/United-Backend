using FluentValidation;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand
{
    public class EnableOtpAuthenticatorCommandValidator : AbstractValidator<EnableOtpAuthenticatorCommand>
    {
        public EnableOtpAuthenticatorCommandValidator()
        {
            RuleFor(x => x.UserID).NotEmpty();
        }
    }
}
