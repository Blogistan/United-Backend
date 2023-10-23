using FluentValidation;

namespace Application.Features.Blogs.Queries.Reports.MostShared
{
    public class MostSharedBlogQueryValidator:AbstractValidator<MostSharedBlogQuery>
    {
        public MostSharedBlogQueryValidator()
        {
            RuleFor(x=>x.DateFilter).NotEmpty();
        }
    }
}
