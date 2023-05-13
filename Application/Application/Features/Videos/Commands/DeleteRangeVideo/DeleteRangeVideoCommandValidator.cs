using FluentValidation;

namespace Application.Features.Videos.Commands.DeleteRangeVideo
{
    public class DeleteRangeVideoCommandValidator:AbstractValidator<DeleteRangeVideoCommand>
    {
        public DeleteRangeVideoCommandValidator()
        {
            RuleFor(x=>x.DeleteRangeDto).NotEmpty();
        }
    }
}
