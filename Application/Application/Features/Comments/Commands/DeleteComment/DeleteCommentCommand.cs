﻿using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand:IRequest<DeleteCommentCommandResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        public bool Permanent { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };

        public class DeleteCommentCommandHandler:IRequestHandler<DeleteCommentCommand, DeleteCommentCommandResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly CommentBusinessRules commentBusinessRules;
            public DeleteCommentCommandHandler(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await commentBusinessRules.CommentCheckById(request.Id);


                Comment  deletedComments = await commentRepository.DeleteAsync(comment, request.Permanent);

                Comment commentWithUser = await commentRepository.GetAsync(x => x.Id==request.Id,x=>x.Include(x=>x.User));

                return new DeleteCommentCommandResponse
                {
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    GuestName = commentWithUser.GuestName,
                    UserName = $"{commentWithUser.User.FirstName} {commentWithUser.User.LastName}",
                    Likes = commentWithUser.Likes,
                    Dislikes = commentWithUser.Dislikes,
                    ParentCommentId=commentWithUser.CommentId
                };
            }
        }
    }
}
