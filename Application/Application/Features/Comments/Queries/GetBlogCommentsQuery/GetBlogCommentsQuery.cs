using Application.Services.Repositories;
using Application.Features.Comments.Rules;
using AutoMapper;
using MediatR;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQuery:IRequest<GetBlogCommentsQueryResponse>
    {
        public int CommentId { get; set; }

        public class GetBlogCommentsQueryHandler:IRequestHandler<GetBlogCommentsQuery, GetBlogCommentsQueryResponse>
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
                
            }
        }
    }
}
