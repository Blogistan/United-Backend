using FluentValidation;

namespace Application.Features.Bans.Queries.GetListBans
{
    public class GetListQueryValidator : AbstractValidator<GetListBansQuery>
    {
        public GetListQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
