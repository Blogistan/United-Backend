using FluentValidation;

namespace Application.Features.Categories.Commands.UpdateRangeCategory
{
    public class UpdateRangeCategoryCommandValidator:AbstractValidator<UpdateRangeCategoryCommand>
    {
        public UpdateRangeCategoryCommandValidator()
        {
            RuleFor(x=>x.UpdateCategoryDtos).NotEmpty();
        }
    }
}
