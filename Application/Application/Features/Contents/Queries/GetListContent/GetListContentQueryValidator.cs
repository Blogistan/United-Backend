using FluentValidation;

namespace Application.Features.Contents.Queries.GetListContent
{
    public class GetListContentQueryValidator : AbstractValidator<GetListContentQuery>
    {
        public GetListContentQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
