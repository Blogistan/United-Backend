using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Queries.GetListBlogDynamic
{
    public class GetListBlogDynamicQuery:IRequest<GetListBlogDynamicQueryResponse>,ISecuredRequest
    {
        public DynamicQuery DynamicQuery { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new string[] { "Admin", "Moderator", "Blogger", "User", "Guest" };

        public class GetListBlogDynamicQueryHandler:IRequestHandler<GetListBlogDynamicQuery, GetListBlogDynamicQueryResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            public GetListBlogDynamicQueryHandler(IBlogRepository blogRepository, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
            }

            public async Task<GetListBlogDynamicQueryResponse> Handle(GetListBlogDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Blog> paginate = await blogRepository.GetListByDynamicAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(x => x.Category).Include(x => x.Writer),dynamic:request.DynamicQuery);

                GetListBlogDynamicQueryResponse response = mapper.Map<GetListBlogDynamicQueryResponse>(paginate);

                return response;
            }
        }
    }
}
