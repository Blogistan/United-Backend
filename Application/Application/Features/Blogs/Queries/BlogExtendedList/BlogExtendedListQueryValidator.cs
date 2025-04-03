using FluentValidation;

namespace Application.Features.Blogs.Queries.BlogExtendedList
{
    public class BlogExtendedListQueryValidator : AbstractValidator<BlogExtendedListQuery>
    {
        public BlogExtendedListQueryValidator()
        {
            RuleFor(x => x.DynamicQuery).NotEmpty();
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
