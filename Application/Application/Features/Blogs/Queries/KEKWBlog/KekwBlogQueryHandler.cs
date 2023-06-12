using FluentValidation;

namespace Application.Features.Blogs.Queries.KEKWBlog
{
    public class KekwBlogQueryValidator:AbstractValidator<KekwBlogQuery>
    {
        public KekwBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
