using FluentValidation;

namespace Application.Features.Auth.Commands.TwitterSignIn
{
    public class TwitterSignInCommandValidator:AbstractValidator<TwitterSignInCommand>
    {
        public TwitterSignInCommandValidator()
        {
            RuleFor(x=>x.AccessToken).NotEmpty();
            RuleFor(x=>x.TokenSecret).NotEmpty();
        }
    }
}
