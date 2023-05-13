using FluentValidation;

namespace Application.Features.Videos.Commands.CreateRangeVideo
{
    public class CreateRangeCommandValidator:AbstractValidator<CreateRangeVideoCommand>
    {
        public CreateRangeCommandValidator()
        {
            RuleFor(x=>x.CreateVideoDtos).NotEmpty();
        }
    }
}
