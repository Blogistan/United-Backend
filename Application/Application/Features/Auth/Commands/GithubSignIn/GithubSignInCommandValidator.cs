using FluentValidation;

namespace Application.Features.Auth.Commands.GithubSignIn
{
    public class GithubSignInCommandValidator:AbstractValidator<GithubSignInCommand>
    {
        public GithubSignInCommandValidator()
        {
            RuleFor(x=>x.Token).NotEmpty();
        }
    }
}
