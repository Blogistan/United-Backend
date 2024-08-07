﻿using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Comments.Rules
{
    public class CommentBusinessRules:BaseBusinessRules
    {
        public readonly ICommentRepository commentRepository;
        public CommentBusinessRules(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<Comment> CommentCheckById(int commentId)
        {
            Comment comment = await commentRepository.GetAsync(x=>x.Id== commentId);
            if (comment is null)
                throw new NotFoundException("Comment is not exist");

            return comment;
        }

    }
}
