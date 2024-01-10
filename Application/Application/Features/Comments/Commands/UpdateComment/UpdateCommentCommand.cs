﻿using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<UpdateCommentResponse>, ISecuredRequest
    {
        public int? UserId { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly CommentBusinessRules commentBusinessRules;
            public UpdateCommentCommandHandler(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<UpdateCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await commentBusinessRules.CommentCheckById((int)request.CommentId);

                comment.CommentContent = request.CommentContent;

                Comment updatedComment = await commentRepository.UpdateAsync(comment);

                Comment commentWithUser = await commentRepository.GetAsync(x => x.UserId == request.UserId, x => x.Include(x => x.User));

                return new UpdateCommentResponse
                {
                    Id = commentWithUser.Id,
                    BlogId = commentWithUser.BlogId,
                    CommentContent = commentWithUser.CommentContent,
                    GuestName = commentWithUser.GuestName,
                    UserName = $"{commentWithUser.User.FirstName} {commentWithUser.User.LastName}",
                    Likes = commentWithUser.Likes,
                    Dislikes = commentWithUser.Dislikes,
                    ParentCommentId = commentWithUser.CommentId
                };
            }
        }
    }
}
