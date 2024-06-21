using FluentValidation;

namespace Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommandValidator:AbstractValidator<DeleteContentCommand>
    {
        public DeleteContentCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
