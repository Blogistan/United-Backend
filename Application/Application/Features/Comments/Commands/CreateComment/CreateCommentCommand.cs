using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CreateCommentCommandResponse>
    {
        public int? UserId { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int? BlogId { get; set; }
        public int? CommentId { get; set; }

        //string[] ISecuredRequest.Roles => new string[] { "User" };

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
                    GuestName = request!.GuestName,
                    Likes = 0,
                    Dislikes = 0,
                    UserId = request!.UserId,
                    CommentId = request.CommentId
                };

                var createdComment = await commentRepository.AddAsync(comment);

                Comment commentWithUser = await commentRepository.GetAsync(x => x.Id == createdComment.Id, x => x.Include(x => x.User));


                return new CreateCommentCommandResponse
                {
                    Id = commentWithUser.Id,
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    UserProfileImageUrl = commentWithUser.User != null ? commentWithUser.User.ProfileImageUrl : "",
                    Dislikes = commentWithUser.Dislikes,
                    GuestName = commentWithUser.GuestName!,
                    Likes = commentWithUser.Likes,
                    ParentCommentId = commentWithUser.CommentId,
                    UserName = commentWithUser.User != null ? $"{commentWithUser.User!.FirstName} {commentWithUser.User!.LastName}" : ""
                };

            }
        }
    }
}
