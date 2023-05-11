using FluentValidation;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommandValidator:AbstractValidator<UpdateVideoCommand>
    {
        public UpdateVideoCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x=>x.VideoUrl).NotEmpty();
            RuleFor(x=>x.WatchCount).GreaterThanOrEqualTo(0);
        }
    }
}
