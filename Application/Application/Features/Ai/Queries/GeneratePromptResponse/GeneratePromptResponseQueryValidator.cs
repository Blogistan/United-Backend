using FluentValidation;

namespace Application.Features.Ai.Queries.GeneratePromptResponse
{
    public class GeneratePromptResponseQueryValidator:AbstractValidator<GeneratePromptResponseQuery>
    {
        public GeneratePromptResponseQueryValidator()
        {
            RuleFor(x=>x.Prompt).NotEmpty();
        }
    }
}
