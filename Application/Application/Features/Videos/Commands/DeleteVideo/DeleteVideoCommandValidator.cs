using FluentValidation;

namespace Application.Features.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommandValidator:AbstractValidator<DeleteVideoCommand>
    {
        public DeleteVideoCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
