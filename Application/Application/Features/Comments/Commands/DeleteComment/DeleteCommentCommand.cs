using Application.Features.Comments.Commands.UpdateComment;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand:IRequest<DeleteCommentCommandResponse>
    {
        public int Id { get; set; }
        public bool Permanent { get; set; }

        public class DeleteCommentCommandHandler:IRequestHandler<DeleteCommentCommand, DeleteCommentCommandResponse>
        {
            private readonly ICommentRepository commentRepository;
            public DeleteCommentCommandHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await commentRepository.GetAsync(x=>x.Id==request.Id);


                Comment  deletedComments = await commentRepository.DeleteAsync(comment, request.Permanent);


                return new DeleteCommentCommandResponse
                {
                    BlogId = deletedComments.BlogId,
                    CommentContent = deletedComments.CommentContent,
                    GuestName = deletedComments.GuestName,
                    UserName = $"{deletedComments.User.FirstName} {deletedComments.User.LastName}",
                    ParentCommentId = deletedComments.ParentCommentId,
                    Likes = deletedComments.Likes,
                    Dislikes = deletedComments.Dislikes
                };
            }
        }
    }
}
