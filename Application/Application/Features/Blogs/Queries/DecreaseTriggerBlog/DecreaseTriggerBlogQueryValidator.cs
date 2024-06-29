using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseTriggerBlog
{
    public class DecreaseTriggerBlogQueryValidator : AbstractValidator<DecreaseTriggerBlogQuery>
    {
        public DecreaseTriggerBlogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
