using Application.Services.Repositories;
using Application.Features.Comments.Rules;
using AutoMapper;
using MediatR;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Features.Comments.Dtos;
using Core.Application.Pipelines.Authorization;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQuery : IRequest<GetBlogCommentsQueryResponse>,ISecuredRequest
    {
        public int BlogId { get; set; }
        public string[] Roles => new string[] { "User" };

        public class GetBlogCommentsQueryHandler : IRequestHandler<GetBlogCommentsQuery, GetBlogCommentsQueryResponse>
        {
            private readonly ICommentRepository commentRepository;
            private readonly IMapper mapper;
            private readonly CommentBusinessRules commentBusinessRules;
            public GetBlogCommentsQueryHandler(ICommentRepository commentRepository, IMapper mapper, CommentBusinessRules commentBusinessRules)
            {
                this.commentRepository = commentRepository;
                this.mapper = mapper;
                this.commentBusinessRules = commentBusinessRules;
            }

            public async Task<GetBlogCommentsQueryResponse> Handle(GetBlogCommentsQuery request, CancellationToken cancellationToken)
            {
                //await commentBusinessRules.CommentCheckById(request.CommentId);

                Comment comment = await commentRepository.GetAsync(x => x.BlogId == request.BlogId, x => x.Include(x => x.User).Include(x => x.CommentResponses));

                CommentViewDto response = mapper.Map<CommentViewDto>(comment);

                return new GetBlogCommentsQueryResponse()
                {
                    commentViewDto = response
                };
            }
        }
    }
}
