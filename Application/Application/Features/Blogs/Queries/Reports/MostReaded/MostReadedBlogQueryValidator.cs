using FluentValidation;

namespace Application.Features.Blogs.Queries.Reports.MostReaded
{
    public class MostReadedBlogQueryValidator:AbstractValidator<MostReadedBlogQuery>
    {
        public MostReadedBlogQueryValidator()
        {
            RuleFor(x=>x.DateFilter).NotEmpty();
        }
    }
}
