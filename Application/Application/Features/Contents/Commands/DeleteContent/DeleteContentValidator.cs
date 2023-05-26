using FluentValidation;

namespace Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentValidator:AbstractValidator<DeleteContentCommand>
    {
        public DeleteContentValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
