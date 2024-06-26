using FluentValidation;

namespace Application.Features.Blogs.Queries.GetListBlog
{
    public class GetListBlogQueryValidator:AbstractValidator<GetListBlogQuery>
    {
        public GetListBlogQueryValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
