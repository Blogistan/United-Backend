using FluentValidation;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public class UpdateBanCommandValidator:AbstractValidator<UpdateBanCommand>
    {
        public UpdateBanCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.IsPerma).NotNull();
            RuleFor(x => x.BanStartDate).NotNull();
            RuleFor(x => x.BanEndDate).NotNull();
            RuleFor(x => x.BanDetail).NotNull();
        }
    }
}
