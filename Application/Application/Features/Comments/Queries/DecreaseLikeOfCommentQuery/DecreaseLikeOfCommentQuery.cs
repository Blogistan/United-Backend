using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

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
                comment.Likes -= 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);

                return new DecreaseLikeOfCommentQueryResponse
                {
                    Id = updatedComment.Id,
                    BlogId = updatedComment.BlogId,
                    CommentContent = updatedComment.CommentContent,
                    Dislikes = updatedComment.Dislikes,
                    GuestName = updatedComment.GuestName,
                    Likes = updatedComment.Likes,
                    UserName = $"{updatedComment.User.FirstName} {updatedComment.User.LastName}"
                };
            }
        }
    }
}
