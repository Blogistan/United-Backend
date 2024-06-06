using FluentValidation;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public class DeleteRangeCategoryCommandValidator:AbstractValidator<DeleteRangeCategoryCommand>
    {
        public DeleteRangeCategoryCommandValidator()
        {
            RuleFor(x=>x.Permanent).NotEmpty();
            RuleFor(x=>x.DeleteRangeCategoryDtos).NotEmpty();
        }
    }
}
