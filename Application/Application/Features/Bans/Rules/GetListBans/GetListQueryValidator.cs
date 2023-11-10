using FluentValidation;

namespace Application.Features.Bans.Rules.GetListBans
{
    public class GetListQueryValidator:AbstractValidator<GetListBansQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
