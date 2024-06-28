using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseTriggerBlog
{
    public class DecreaseTriggerBlogQueryHandler : AbstractValidator<DecreaseTriggerBlogQuery>
    {
        public DecreaseTriggerBlogQueryHandler()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
