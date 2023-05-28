using FluentValidation;

namespace Application.Features.Blogs.Queries.GetListBlog
{
    public class GetListBlogValidator:AbstractValidator<GetListBlogQuery>
    {
        public GetListBlogValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
