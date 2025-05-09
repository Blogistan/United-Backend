using Core.Application.Pipelines.Authorization;
using Infrastructure.HuggingFace.Abstract;
using MediatR;

namespace Application.Features.Ai.Queries.STT
{
    public class SpeechToTextQuery : IRequest<SpeechToTextResponse>,ISecuredRequest
    {
        public byte[] AudioBytes { get; set; }
        string[] ISecuredRequest.Roles => new string[] {"Admin","Moderator","User"}; 

        public class SpeechToTextQueryHandler: IRequestHandler<SpeechToTextQuery, SpeechToTextResponse>
        {
            private readonly IHuggingFaceService huggingFaceService;

            public SpeechToTextQueryHandler(IHuggingFaceService huggingFaceService)
            {
                this.huggingFaceService = huggingFaceService;
            }

            public async Task<SpeechToTextResponse> Handle(SpeechToTextQuery request, CancellationToken cancellationToken)
            {
                var text = await huggingFaceService.SpeechToTextAsync(request.AudioBytes);
                return new SpeechToTextResponse(text);
            }
        }
    }
}
