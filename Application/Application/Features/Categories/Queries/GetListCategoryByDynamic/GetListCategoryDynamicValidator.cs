using FluentValidation;

namespace Application.Features.Categories.Queries.GetListCategoryByDynamic
{
    public class GetListCategoryDynamicValidator:AbstractValidator<GetListCategoryQueryByDynamicQuery>
    {
        public GetListCategoryDynamicValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
           
        }
    }
}
