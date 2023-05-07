using FluentValidation;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryValidator:AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
