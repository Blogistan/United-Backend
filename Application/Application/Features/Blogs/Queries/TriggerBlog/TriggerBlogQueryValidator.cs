using FluentValidation;

namespace Application.Features.Blogs.Queries.TriggerBlog
{
    public class TriggerBlogQueryValidator:AbstractValidator<TriggerBlogQuery>
    {
        public TriggerBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
