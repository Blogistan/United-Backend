using FluentValidation;

namespace Application.Features.Blogs.Queries.ShareBlog
{
    public class ShareBlogQueryValidator:AbstractValidator<ShareBlogQuery>
    {
        public ShareBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
