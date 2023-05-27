using FluentValidation;

namespace Application.Features.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommandValidator:AbstractValidator<DeleteBlogCommand>
    {
        public DeleteBlogCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
