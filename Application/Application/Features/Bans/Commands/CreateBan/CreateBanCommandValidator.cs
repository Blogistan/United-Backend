using FluentValidation;

namespace Application.Features.Bans.Commands.CreateBan
{
    public class CreateBanCommandValidator:AbstractValidator<CreateBanCommand>
    {
        public CreateBanCommandValidator()
        {
            RuleFor(x=>x.UserID).NotEmpty();
            RuleFor(x=>x.IsPerma).NotNull();
            RuleFor(x=>x.BanStartDate).NotEmpty();
            RuleFor(x=>x.BanEndDate).NotEmpty();
            RuleFor(x=>x.BanDetail).NotEmpty();
        }
    }
}
