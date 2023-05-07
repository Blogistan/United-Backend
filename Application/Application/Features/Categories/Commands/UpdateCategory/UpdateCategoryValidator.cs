using FluentValidation;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }
}
