using FluentValidation;

namespace Application.Features.Videos.Commands.CreateRangeVideo
{
    public class CreateRangeVideoCommandValidator:AbstractValidator<CreateRangeVideoCommand>
    {
        public CreateRangeVideoCommandValidator()
        {
            RuleFor(x=>x.CreateVideoDtos).NotEmpty();
        }
    }
}
