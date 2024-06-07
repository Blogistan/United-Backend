using FluentValidation;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public class UpdateBanCommandValidator : AbstractValidator<UpdateBanCommand>
    {
        public UpdateBanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.IsPerma).NotNull();
            RuleFor(x => x.BanStartDate).NotEmpty();
            RuleFor(x => x.BanEndDate).NotEmpty();
            RuleFor(x => x.BanDetail).NotEmpty();
        }
    }
}
