using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseSuprisedBlog
{
    public class DecreaseSurprisedBlogQueryValidator : AbstractValidator<DecreaseSurprisedBlogQuery>
    {
        public DecreaseSurprisedBlogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
