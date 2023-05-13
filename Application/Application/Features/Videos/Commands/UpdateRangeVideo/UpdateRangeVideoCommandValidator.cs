using FluentValidation;

namespace Application.Features.Videos.Commands.UpdateRangeVideo
{
    public class UpdateRangeVideoCommandValidator : AbstractValidator<UpdateRangeVideoCommand>
    {
        public UpdateRangeVideoCommandValidator()
        {
            RuleFor(x => x.updateVideoDtos).NotEmpty();
        }
    }
}
