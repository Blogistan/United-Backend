using FluentValidation;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQueryValidator:AbstractValidator<GetBlogCommentsQuery>
    {
        public GetBlogCommentsQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
