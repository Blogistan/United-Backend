using FluentValidation;

namespace Application.Features.Contents.Commands.CreateContent
{
    public class CreateContentCommandValidator:AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x=>x.ContentPragraph).NotEmpty();
        }
    }
}
