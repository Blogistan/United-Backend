using Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery
{
    public class DecreaseDislikeOfCommentQuery:IRequest<DecreaseDislikeOfCommentCommentQueryResponse>,ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };


        public class DecreaseDislikeOfCommentQueryHandler:IRequestHandler<DecreaseDislikeOfCommentQuery, DecreaseDislikeOfCommentCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            public DecreaseDislikeOfCommentQueryHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<DecreaseDislikeOfCommentCommentQueryResponse> Handle(DecreaseDislikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.GetAsync(x => x.Id == request.CommentId);


                comment.Likes -= 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);

                return new DecreaseDislikeOfCommentCommentQueryResponse
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
