using FluentValidation;

namespace Application.Features.Contents.Commands.UpdateContent
{
    public class UpdateContentCommandValidator:AbstractValidator<UpdateContentCommand>
    {
        public UpdateContentCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.ContentPragraph).NotEmpty();
        }
    }
}
