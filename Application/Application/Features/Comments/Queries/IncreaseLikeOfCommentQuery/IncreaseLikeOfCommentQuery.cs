using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery
{
    public class IncreaseLikeOfCommentQuery:IRequest<IncreaseLikeOfCommentQueryResponse>,ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };

        public class IncreaseLikeOfCommentQueryHandler:IRequestHandler<IncreaseLikeOfCommentQuery, IncreaseLikeOfCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            public IncreaseLikeOfCommentQueryHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<IncreaseLikeOfCommentQueryResponse> Handle(IncreaseLikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.GetAsync(x=>x.Id==request.CommentId);


                comment.Likes += 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);

                return new IncreaseLikeOfCommentQueryResponse
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
