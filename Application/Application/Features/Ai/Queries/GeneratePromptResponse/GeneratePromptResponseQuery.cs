using Infrastructure.OpenAI.Abstract;
using MediatR;

namespace Application.Features.Ai.Queries.GeneratePromptResponse
{
    public class GeneratePromptResponseQuery : IRequest<GeneratePromptResponseQueryResponse>
    {
        public string Prompt { get; set; }

        public class GeneratePromptResponseQueryHandler : IRequestHandler<GeneratePromptResponseQuery, GeneratePromptResponseQueryResponse>
        {
            private readonly IAiService aiService;
            public GeneratePromptResponseQueryHandler(IAiService aiService)
            {
                this.aiService = aiService;
            }
            public async Task<GeneratePromptResponseQueryResponse> Handle(GeneratePromptResponseQuery request, CancellationToken cancellationToken)
            {
                var message = await aiService.GenerateResponse(request.Prompt);

                return new GeneratePromptResponseQueryResponse { Message = message };
            }
        }
    }
}
