﻿using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery
{
    public class DecreaseDislikeOfCommentQuery : IRequest<DecreaseDislikeOfCommentCommentQueryResponse>, ISecuredRequest
    {
        public int CommentId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User" };


        public class DecreaseDislikeOfCommentQueryHandler : IRequestHandler<DecreaseDislikeOfCommentQuery, DecreaseDislikeOfCommentCommentQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly CommentBusinessRules commentBusinessRules;
            public DecreaseDislikeOfCommentQueryHandler(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<DecreaseDislikeOfCommentCommentQueryResponse> Handle(DecreaseDislikeOfCommentQuery request, CancellationToken cancellationToken)
            {
                var comment = await commentBusinessRules.CommentCheckById(request.CommentId);
                comment.Likes -= 1;

                var updatedComment = await commentRepository.UpdateAsync(comment);

                return new DecreaseDislikeOfCommentCommentQueryResponse
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
