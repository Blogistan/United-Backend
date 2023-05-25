using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommand : IRequest<DeleteContentCommandResponse>
    {
        public int Id { get; set; }

        public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, DeleteContentCommandResponse>
        {
            private readonly IContentRepository contentRepository;
            public DeleteContentCommandHandler(IContentRepository contentRepository)
            {
                this.contentRepository = contentRepository;
            }

            public async Task<DeleteContentCommandResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
            {
                var content = await contentRepository.GetAsync(x => x.Id == request.Id);

                Content deletedContent = await contentRepository.DeleteAsync(content);

                return new DeleteContentCommandResponse
                {

                    Id = deletedContent.Id,
                    Title = deletedContent.Title,
                    ContentImageUrl = deletedContent.ContentImageUrl,
                    ContentPragraph = deletedContent.ContentPragraph

                };
            }
        }
    }
}
