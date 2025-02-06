using Infrastructure.OpenAI.Abstract;
using MediatR;

namespace Application.Features.Ai.Queries.GeneratePromptResponse
{
    public class GeneratePromptResponseQuery : IRequest<GeneratePromptResponseQueryResponse>
    {
        public string Prompt { get; set; }

        public class GeneratePromptResponseQueryHandler : IRequestHandler<GeneratePromptResponseQuery, GeneratePromptResponseQueryResponse>
        {
            private readonly IOpenAiService openAiService;
            public GeneratePromptResponseQueryHandler(IOpenAiService openAiService)
            {
                this.openAiService = openAiService;
            }
            public async Task<GeneratePromptResponseQueryResponse> Handle(GeneratePromptResponseQuery request, CancellationToken cancellationToken)
            {
                var message = await openAiService.GenerateResponse(request.Prompt);

                return new GeneratePromptResponseQueryResponse { Message = message };
            }
        }
    }
}
