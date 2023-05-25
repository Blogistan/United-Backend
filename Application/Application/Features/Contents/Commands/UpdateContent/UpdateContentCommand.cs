using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Commands.UpdateContent
{
    public class UpdateContentCommand : IRequest<UpdateContentResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }

        public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, UpdateContentResponse>
        {
            private readonly IContentRepository contentRepository;
            public UpdateContentCommandHandler(IContentRepository contentRepository)
            {
                this.contentRepository = contentRepository;
            }

            public async Task<UpdateContentResponse> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
            {
                Content content = new()
                {
                    Id = request.Id,
                    Title = request.Title,
                    ContentImageUrl = request.ContentImageUrl,
                    ContentPragraph = request.ContentPragraph
                };

                Content updatedContent = await contentRepository.UpdateAsync(content);

                return new UpdateContentResponse
                {
                    Id = updatedContent.Id,
                    Title = updatedContent.Title,
                    ContentImageUrl = updatedContent.ContentImageUrl,
                    ContentPragraph = updatedContent.ContentPragraph
                };

            }
        }
    }
}
