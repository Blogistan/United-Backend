using FluentValidation;

namespace Application.Features.Blogs.Queries.LikeBlog
{
    public class ReadBlogQueryValidator:AbstractValidator<ReadBlogQuery>
    {
        public ReadBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
