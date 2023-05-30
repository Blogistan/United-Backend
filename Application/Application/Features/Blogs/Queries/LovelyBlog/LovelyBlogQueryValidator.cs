using FluentValidation;

namespace Application.Features.Blogs.Queries.LovelyBlog
{
    public class LovelyBlogQueryValidator:AbstractValidator<LovelyBlogQuery>
    {
        public LovelyBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
