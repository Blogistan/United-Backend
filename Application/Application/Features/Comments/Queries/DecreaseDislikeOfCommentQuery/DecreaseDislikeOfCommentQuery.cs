using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery
{
    public class DecreaseDislikeOfCommentQuery : IRequest<DecreaseDislikeOfCommentCommentQueryResponse>, ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };


        public class DecreaseDislikeOfCommentQueryHandler : IRequestHandler<DecreaseDislikeOfCommentQuery, DecreaseDislikeOfCommentCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly CommentBusinessRules commentBusinessRules;
            public DecreaseDislikeOfCommentQueryHandler(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<DecreaseDislikeOfCommentCommentQueryResponse> Handle(DecreaseDislikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentBusinessRules.CommentCheckById(request.CommentId);
                comment.Dislikes = comment.Dislikes == 0 ? 0 : comment.Dislikes - 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);
                var commentWithUser = await commentRepository.GetAsync(x => x.Id == updatedComment.Id, x => x.Include(x => x.SiteUser).ThenInclude(x=>x.User));

                return new DecreaseDislikeOfCommentCommentQueryResponse
                {
                    Id = commentWithUser.Id,
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    ProfileImageUrl = commentWithUser.SiteUser != null ? commentWithUser.SiteUser.ProfileImageUrl : null,
                    Dislikes = commentWithUser.Dislikes,
                    GuestName = commentWithUser.GuestName!,
                    Likes = commentWithUser.Likes,
                    CommentId = commentWithUser.CommentId,
                    UserName = commentWithUser.SiteUser != null ? $"{commentWithUser.SiteUser.User!.FirstName} {commentWithUser.SiteUser.User!.LastName}" : null,
                    CreateDate = commentWithUser.CreatedDate,
                    CommentResponses = new List<Dtos.CommentViewDto>()
                };
            }
        }

    }
}
