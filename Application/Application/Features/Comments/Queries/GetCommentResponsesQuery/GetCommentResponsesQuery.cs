using Application.Features.Comments.Dtos;
using Application.Features.Comments.Queries.GetBlogCommentsQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.GetCommentResponsesQuery
{
    public class GetCommentResponsesQuery:IRequest<GetBlogCommentsQueryResponse>
    {
        public int CommentId { get; set; }

        public class GetCommentResponsesQueryHandler:IRequestHandler<GetCommentResponsesQuery, GetBlogCommentsQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly IMapper mapper;
            private CommentBusinessRules CommentBusinessRules;
            public GetCommentResponsesQueryHandler(ICommentRepository commentRepository, IMapper mapper, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.mapper = mapper;
                CommentBusinessRules = commentBusinessRules;
            }

            public async Task<GetBlogCommentsQueryResponse> Handle(GetCommentResponsesQuery request, CancellationToken cancellationToken)
            {
                await CommentBusinessRules.CommentCheckById(request.CommentId);

                Comment comment = await commentRepository.GetAsync(x=>x.Id==request.CommentId,x=>x.Include(x=>x.CommentResponses).Include(x=>x.User));

                GetBlogCommentsQueryResponse response = mapper.Map<GetBlogCommentsQueryResponse>(comment);

                return response;
            }
        }
    }
}
