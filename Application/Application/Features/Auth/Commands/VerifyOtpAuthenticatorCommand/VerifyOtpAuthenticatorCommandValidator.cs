using FluentValidation;

namespace Application.Features.Auth.Commands.VerifyOtpAuthenticatorCommand
{
    public class VerifyOtpAuthenticatorCommandValidator : AbstractValidator<VerifyOtpAuthenticatorCommand>
    {
        public VerifyOtpAuthenticatorCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OtpCode).NotEmpty();
        }
    }
}
