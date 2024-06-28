using FluentValidation;

namespace Application.Features.Blogs.Queries.DecreaseLovelyBlog
{
    public class DecreaseLovelyBLogQueryValidator : AbstractValidator<DecreaseLovelyBLogQuery>
    {
        public DecreaseLovelyBLogQueryValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
        }
    }
}
