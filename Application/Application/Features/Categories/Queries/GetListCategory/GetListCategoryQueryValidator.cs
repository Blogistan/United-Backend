using FluentValidation;

namespace Application.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQueryValidator:AbstractValidator<GetListCategoryQuery>
    {
        public GetListCategoryQueryValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
