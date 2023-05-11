using FluentValidation;

namespace Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommandValidator:AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x =>x.VideoUrl).NotEmpty();
            
        }
    }
}
