using FluentValidation;

namespace Application.Features.Ai.Queries.STT
{
    public class SpeechToTextQueryValidator : AbstractValidator<SpeechToTextQuery>
    {

        public SpeechToTextQueryValidator()
        {
            RuleFor(x => x.AudioBytes)
                .NotNull()
                .Must(x => x.Length > 0);
        }
    }
}
