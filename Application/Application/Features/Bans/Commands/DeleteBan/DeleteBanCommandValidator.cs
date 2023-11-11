using FluentValidation;

namespace Application.Features.Bans.Commands.DeleteBan
{
    public class DeleteBanCommandValidator:AbstractValidator<DeleteBanCommand>
    {
        public DeleteBanCommandValidator()
        {
            RuleFor(x=>x.ReportID).NotEmpty();
        }
    }
}
