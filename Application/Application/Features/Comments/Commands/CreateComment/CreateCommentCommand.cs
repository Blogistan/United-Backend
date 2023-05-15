using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CreateCommentCommandResponse>
    {
        public int? UserId { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int? ParentCommentId { get; set; }
        public int? BlogId { get; set; }


        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandResponse>
        {
            private readonly ICommentRepository commentRepository;
            public CreateCommentCommandHandler(ICommentRepository commentRepository)
            {
                this.commentRepository = commentRepository;
            }

            public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                Comment comment = new()
                {
                    BlogId = request.BlogId,
                    CommentContent = request.CommentContent,
                    GuestName = request.GuestName,
                    ParentCommentId = request.ParentCommentId,
                    Likes = 0,
                    Dislikes = 0,
                    UserId = request.UserId
                };

                Comment createdComment = await commentRepository.AddAsync(comment);


                return new CreateCommentCommandResponse
                {
                    BlogId = createdComment.BlogId,
                    CommentContent = createdComment.CommentContent,
                    Dislikes = createdComment.Dislikes,
                    GuestName = createdComment.GuestName,
                    Likes = createdComment.Likes,
                    UserName = $"{createdComment.User.FirstName} {createdComment.User.LastName}"
                };

            }
        }
    }
}
