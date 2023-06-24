using FluentValidation;

namespace Application.Features.Blogs.Queries.BlogDetailById
{
    public class BlogDetailByIdQueryValidator:AbstractValidator<BlogDetailByIdQuery>
    {
        public BlogDetailByIdQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
