using FluentValidation;

namespace Application.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandValidator:AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x=>x.WriterId).NotEmpty();
            RuleFor(x => x.BannerImageUrl).NotEmpty();
            RuleFor(x=>x.CategoryId).NotEmpty();
        }
    }
}
