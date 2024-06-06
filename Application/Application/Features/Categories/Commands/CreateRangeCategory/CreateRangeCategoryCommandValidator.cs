using FluentValidation;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public class CreateRangeCategoryCommandValidator:AbstractValidator<CreateRangeCategoryCommand>
    {
        public CreateRangeCategoryCommandValidator()
        {
            RuleFor(x=>x.CreateCategoryDtos).NotEmpty();
        }
    }
}
