﻿using Application.Features.Comments.Commands.UpdateComment;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

                Comment commentWithUser = await commentRepository.GetAsync(x => x.Id==request.Id,x=>x.Include(x=>x.User));

                return new DeleteCommentCommandResponse
                {
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    GuestName = commentWithUser.GuestName,
                    UserName = $"{commentWithUser.User.FirstName} {commentWithUser.User.LastName}",
                    Likes = commentWithUser.Likes,
                    Dislikes = commentWithUser.Dislikes
                };
            }
        }
    }
}
