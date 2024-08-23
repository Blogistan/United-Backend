using FluentValidation;

namespace Application.Features.Blogs.Queries.SuprisedBlog
{
    public class SurprisedBlogQueryValidator:AbstractValidator<SurprisedBlogQuery>
    {
        public SurprisedBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
