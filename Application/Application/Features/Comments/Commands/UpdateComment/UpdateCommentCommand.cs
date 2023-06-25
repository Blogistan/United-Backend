using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

                Comment commentWithUser = await commentRepository.GetAsync(x => x.UserId == request.UserId, x => x.Include(x => x.User));

                return new UpdateCommentResponse
                {
                    Id= commentWithUser.Id,
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    GuestName = commentWithUser.GuestName,
                    UserName = $"{commentWithUser.User.FirstName} {commentWithUser.User.LastName}",
                    ParentCommentId = commentWithUser.ParentCommentId,
                    Likes = commentWithUser.Likes,
                    Dislikes = commentWithUser.Dislikes
                };
            }
        }
    }
}
