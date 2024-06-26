using FluentValidation;

namespace Application.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandValidator:AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.WriterId).NotEmpty();
            RuleFor(x => x.BannerImageUrl).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
