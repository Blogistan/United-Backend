using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<UpdateCommentResponse>
    {
        public int? UserId { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }
        public int? BlogId { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentResponse>
        {
            private readonly ICommentRepository commentRepository;
            public UpdateCommentCommandHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<UpdateCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                Comment comment = new()
                {

                    BlogId = request.BlogId,
                    CommentContent = request.CommentContent,
                    UserId = request.UserId,
                    GuestName = request.GuestName,
                    Likes = request.Likes,
                    Dislikes = request.Dislikes,
                    ParentCommentId = request.ParentCommentId
                };

                Comment updatedComment = await commentRepository.UpdateAsync(comment);


                return new UpdateCommentResponse
                {
                    BlogId = updatedComment.BlogId,
                    CommentContent = updatedComment.CommentContent,
                    GuestName = updatedComment.GuestName,
                    UserName = $"{updatedComment.User.FirstName} {updatedComment.User.LastName}",
                    ParentCommentId = updatedComment.ParentCommentId,
                    Likes = updatedComment.Likes,
                    Dislikes = updatedComment.Dislikes
                };
            }
        }
    }
}
