using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseSadBlog
{
    public class DecreaseSadBlogQueryValidator : AbstractValidator<DecreaseSadBlogQuery>
    {
        public DecreaseSadBlogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
