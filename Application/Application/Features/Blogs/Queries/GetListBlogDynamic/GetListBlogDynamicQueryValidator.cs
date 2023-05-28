using FluentValidation;

namespace Application.Features.Blogs.Queries.GetListBlogDynamic
{
    public class GetListBlogDynamicQueryValidator:AbstractValidator<GetListBlogDynamicQuery>
    {
        public GetListBlogDynamicQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
