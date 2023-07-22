using FluentValidation;

namespace Application.Features.Auth.Commands.ForgetPassword
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
