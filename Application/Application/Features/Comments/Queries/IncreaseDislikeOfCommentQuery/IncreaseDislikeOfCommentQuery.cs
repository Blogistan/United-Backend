using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery
{
    public class IncreaseDislikeOfCommentQuery : IRequest<IncreaseDislikeOfCommentQueryResponse>, ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };

        public class IncreaseDislikeOfCommentQueryHandler : IRequestHandler<IncreaseDislikeOfCommentQuery, IncreaseDislikeOfCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;

            public IncreaseDislikeOfCommentQueryHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<IncreaseDislikeOfCommentQueryResponse> Handle(IncreaseDislikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.GetAsync(x => x.Id == request.CommentId);


                comment.Dislikes++;

                var updatedComment = await commentRepository.UpdateAsync(comment);
                var commentWithUser = await commentRepository.GetAsync(x => x.Id == updatedComment.Id, x => x.Include(x => x.SiteUser).ThenInclude(x=>x.User));

                return new IncreaseDislikeOfCommentQueryResponse
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
