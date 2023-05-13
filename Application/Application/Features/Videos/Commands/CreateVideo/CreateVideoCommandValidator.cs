using FluentValidation;

namespace Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommandValidator:AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(x=>x.CreateVideoDto.Title).NotEmpty();
            RuleFor(x =>x.CreateVideoDto.VideoUrl).NotEmpty();
            
        }
    }
}
