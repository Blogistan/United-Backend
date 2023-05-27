using FluentValidation;

namespace Application.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandValidator:AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
