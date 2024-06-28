using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseKEKWBlog
{
    public class DecreaseKEKWBlogQueryValidator : AbstractValidator<DecreaseKEKWBlogQuery>
    {
        public DecreaseKEKWBlogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
