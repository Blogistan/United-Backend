using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Commands.UpdateContent
{
    public class UpdateContentCommand : IRequest<UpdateContentCommandResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

        public class UpdateContentCommandHandler : IRequestHandler<UpdateContentCommand, UpdateContentCommandResponse>
        {
            private readonly IContentRepository contentRepository;
            private readonly ContentBusinessRules contentBusinessRules;
            public UpdateContentCommandHandler(IContentRepository contentRepository, ContentBusinessRules contentBusinessRules)
            {
                this.contentRepository = contentRepository;
                this.contentBusinessRules = contentBusinessRules;
            }

            public async Task<UpdateContentCommandResponse> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
            {
                var content = await contentBusinessRules.ContentCheckById(request.Id);

                content.Id = request.Id;
                content.Title = request.Title;
                content.ContentImageUrl = request.ContentImageUrl;
                content.ContentPragraph = request.ContentPragraph;



                Content updatedContent = await contentRepository.UpdateAsync(content);

                return new UpdateContentCommandResponse
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
