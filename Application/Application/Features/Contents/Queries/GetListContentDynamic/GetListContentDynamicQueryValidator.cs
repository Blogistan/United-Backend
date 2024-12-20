using FluentValidation;

namespace Application.Features.Contents.Queries.GetListContentDynamic
{
    public class GetListContentDynamicQueryValidator : AbstractValidator<GetListContentDynamicQuery>
    {
        public GetListContentDynamicQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
