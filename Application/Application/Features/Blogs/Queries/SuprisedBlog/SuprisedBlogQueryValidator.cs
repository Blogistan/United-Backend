using FluentValidation;

namespace Application.Features.Blogs.Queries.SuprisedBlog
{
    public class SuprisedBlogQueryValidator:AbstractValidator<SuprisedBlogQuery>
    {
        public SuprisedBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
