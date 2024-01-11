using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Commands.CreateContent
{
    public class CreateContentCommand : IRequest<CreateContentCommandResponse>, ISecuredRequest
    {
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

        public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, CreateContentCommandResponse>
        {
            private readonly IContentRepository contentRepository;
            public CreateContentCommandHandler(IContentRepository contentRepository)
            {
                this.contentRepository = contentRepository;
            }

            public async Task<CreateContentCommandResponse> Handle(CreateContentCommand request, CancellationToken cancellationToken)
            {
                Content content = new()
                {
                    Title = request.Title,
                    ContentImageUrl = request.ContentImageUrl,
                    ContentPragraph = request.ContentPragraph
                };

                Content createdContent = await contentRepository.AddAsync(content);

                return new CreateContentCommandResponse
                {

                    Id = createdContent.Id,
                    Title = createdContent.Title,
                    ContentImageUrl = createdContent.ContentImageUrl,
                    ContentPragraph = request.ContentPragraph
                };

            }
        }
    }
}
