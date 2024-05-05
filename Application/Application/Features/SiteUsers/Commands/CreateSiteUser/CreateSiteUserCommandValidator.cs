using FluentValidation;

namespace Application.Features.SiteUsers.Commands.CreateSiteUser
{
    public class CreateSiteUserCommandValidator : AbstractValidator<CreateSiteUserCommand>
    {
        public CreateSiteUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}
