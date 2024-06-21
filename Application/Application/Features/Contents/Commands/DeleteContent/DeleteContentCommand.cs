using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommand : IRequest<DeleteContentCommandResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

        public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, DeleteContentCommandResponse>
        {
            private readonly IContentRepository contentRepository;
            private readonly ContentBusinessRules contentBusinessRules;
            public DeleteContentCommandHandler(IContentRepository contentRepository, ContentBusinessRules contentBusinessRules)
            {
                this.contentRepository = contentRepository;
                this.contentBusinessRules = contentBusinessRules;
            }

            public async Task<DeleteContentCommandResponse> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
            {
                var content = await contentBusinessRules.ContentCheckById(request.Id);

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
