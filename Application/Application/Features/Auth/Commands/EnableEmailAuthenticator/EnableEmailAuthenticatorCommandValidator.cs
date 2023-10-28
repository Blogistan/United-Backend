using FluentValidation;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator
{
    public class EnableEmailAuthenticatorCommandValidator:AbstractValidator<EnableEmailAuthenticatorCommand>
    {
        public EnableEmailAuthenticatorCommandValidator()
        {
            RuleFor(x=>x.UserID).NotEmpty();    
        }
    }
}
