using FluentValidation;

namespace Application.Features.SiteUsers.Commands.DeleteSiteUser
{
    public class DeleteSiteUserCommandValidator:AbstractValidator<DeleteSiteUserCommand>
    {
        public DeleteSiteUserCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
