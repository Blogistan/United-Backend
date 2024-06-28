using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseSuprisedBlog
{
    public class DecreaseSuprisedBlogQueryValidator : AbstractValidator<DecreaseSuprisedBlogQuery>
    {
        public DecreaseSuprisedBlogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
