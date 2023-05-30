using FluentValidation;

namespace Application.Features.Blogs.Queries.SadBlog
{
    public class SadBlogQueryValidator:AbstractValidator<SadBlogQuery>
    {
        public SadBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
