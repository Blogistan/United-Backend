using FluentValidation;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public class DeleteRangeCategoryValidator:AbstractValidator<DeleteRangeCategoryCommand>
    {
        public DeleteRangeCategoryValidator()
        {
            RuleFor(x=>x.Permanent).NotEmpty();
            RuleFor(x=>x.DeleteRangeCategoryDtos).NotEmpty();
        }
    }
}
