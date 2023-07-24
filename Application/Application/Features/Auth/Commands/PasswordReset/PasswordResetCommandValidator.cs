using FluentValidation;

namespace Application.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetCommandValidator : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetCommandValidator()
        {
            RuleFor(x => x.NewPassword).Equal(x => x.NewPasswordConfirm);
        }
    }
}
