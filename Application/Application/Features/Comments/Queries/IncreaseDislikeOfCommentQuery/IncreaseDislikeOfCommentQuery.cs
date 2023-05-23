using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery
{
    public class IncreaseDislikeOfCommentQuery:IRequest<IncreaseDislikeOfCommentQueryResponse>
    {
        public int CommentId { get; set; }

        public class IncreaseDislikeOfCommentQueryHandler:IRequestHandler<IncreaseDislikeOfCommentQuery, IncreaseDislikeOfCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;

            public IncreaseDislikeOfCommentQueryHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<IncreaseDislikeOfCommentQueryResponse> Handle(IncreaseDislikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.GetAsync(x => x.Id == request.CommentId);


                comment.Dislikes += 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);

                return new IncreaseDislikeOfCommentQueryResponse
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
