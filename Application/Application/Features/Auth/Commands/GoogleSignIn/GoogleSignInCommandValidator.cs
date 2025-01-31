using FluentValidation;

namespace Application.Features.Auth.Commands.GoogleSignIn
{
    public class GoogleSignInCommandValidator:AbstractValidator<GoogleSignInCommand>
    {
        public GoogleSignInCommandValidator()
        {

            RuleFor(x=>x.Code).NotEmpty();
            RuleFor(x=>x.IpAdress).NotEmpty();
        }
    }
}
