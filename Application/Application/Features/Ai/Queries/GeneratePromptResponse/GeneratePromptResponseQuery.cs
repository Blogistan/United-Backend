using Application.Services.Assistant.Services;
using MediatR;

namespace Application.Features.Ai.Queries.GeneratePromptResponse
{
    public class GeneratePromptResponseQuery : IRequest<Unit>
    {
        public string Prompt { get; set; }
        public string ConnectionId { get; set; }

        public class GeneratePromptResponseQueryHandler: IRequestHandler<GeneratePromptResponseQuery, Unit>
        {
            private readonly AiService aiService;
            public GeneratePromptResponseQueryHandler(AiService aiService)
            {
                this.aiService = aiService;;
            }
            public async Task<Unit> Handle(GeneratePromptResponseQuery request, CancellationToken cancellationToken)
            {
                await aiService.GetMessageStreamAsync(request.Prompt, request.ConnectionId,cancellationToken);
                return Unit.Value;
            }
        }
    }
}
