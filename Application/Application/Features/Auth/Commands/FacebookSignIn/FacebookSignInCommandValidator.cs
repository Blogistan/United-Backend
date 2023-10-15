using FluentValidation;

namespace Application.Features.Auth.Commands.FacebookSignIn
{
    public class FacebookSignInCommandValidator:AbstractValidator<FacebookSignInCommand>
    {
        public FacebookSignInCommandValidator()
        {
            RuleFor(x=>x.Token).NotEmpty();
        }
    }
}
