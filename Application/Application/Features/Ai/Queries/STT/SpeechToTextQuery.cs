using Infrastructure.HuggingFace.Abstract;
using MediatR;

namespace Application.Features.Ai.Queries.STT
{
    public class SpeechToTextQuery : IRequest<SpeechToTextResponse>
    {
        public byte[] AudioBytes { get; set; }

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
