using Application.Features.Comments.Dtos;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQuery : IRequest<GetBlogCommentsQueryResponse>
    {
        public int BlogId { get; set; }
        //string[] ISecuredRequest.Roles => new string[] { "User" };

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

                IPaginate<Comment> comment = await commentRepository.GetListAsync(x => x.BlogId == request.BlogId, include: x => x.Include(x => x.User).Include(x => x.CommentResponses));

                GetBlogCommentsQueryResponse response = mapper.Map<GetBlogCommentsQueryResponse>(comment);

                return response;
            }
        }
    }
}
