using FluentValidation;

namespace Application.Features.SiteUsers.Commands.UpdateSiteUser
{
    public class UpdateSiteUserCommandValidator:AbstractValidator<UpdateSiteUserCommand>
    {
        public UpdateSiteUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            //RuleFor(x => x.OldPassword).MinimumLength(6).NotEmpty();
            //RuleFor(x => x.NewPassword).MinimumLength(6).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
