﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Blogs.Queries.GetListBlog
{
    public class GetListBlogQuery : IRequest<GetListBlogQueryResponse>/*,ISecuredRequest*/
    {
        public PageRequest PageRequest { get; set; }

        //public string[] Roles => new string[] { "Admin", "Moderator", "Blogger", "User", "Guest" };

        public class GetListBlogQueryHandler : IRequestHandler<GetListBlogQuery, GetListBlogQueryResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            public GetListBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
            }

            public async Task<GetListBlogQueryResponse> Handle(GetListBlogQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Blog> paginate = await blogRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(x => x.Category).Include(x => x.Writer).ThenInclude(x => x.Bans));

                GetListBlogQueryResponse response = mapper.Map<GetListBlogQueryResponse>(paginate);

                return response;
            }
        }
    }
}
