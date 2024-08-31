using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery
{
    public class DecreaseLikeOfCommentQuery : IRequest<DecreaseLikeOfCommentQueryResponse>, ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };

        public class DecreaseLikeOfCommentQueryHandler : IRequestHandler<DecreaseLikeOfCommentQuery, DecreaseLikeOfCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly CommentBusinessRules commentBusinessRules;
            public DecreaseLikeOfCommentQueryHandler(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<DecreaseLikeOfCommentQueryResponse> Handle(DecreaseLikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentBusinessRules.CommentCheckById(request.CommentId);
                comment.Likes = comment.Likes == 0 ? 0 : comment.Likes - 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);
                var commentWithUser = await commentRepository.GetAsync(x => x.Id == updatedComment.Id, x => x.Include(x => x.User));

                return new DecreaseLikeOfCommentQueryResponse
                {
                    Id = commentWithUser.Id,
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    ProfileImageUrl = commentWithUser.User != null ? commentWithUser.User.ProfileImageUrl : null,
                    Dislikes = commentWithUser.Dislikes,
                    GuestName = commentWithUser.GuestName!,
                    Likes = commentWithUser.Likes,
                    CommentId = commentWithUser.CommentId,
                    UserName = commentWithUser.User != null ? $"{commentWithUser.User!.FirstName} {commentWithUser.User!.LastName}" : null,
                    CreateDate = commentWithUser.CreatedDate,
                    CommentResponses = new List<Dtos.CommentViewDto>()
                };
            }
        }
    }
}
