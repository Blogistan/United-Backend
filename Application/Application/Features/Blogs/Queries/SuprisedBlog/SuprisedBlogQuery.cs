﻿using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Blogs.Queries.SuprisedBlog
{
    public class SuprisedBlogQuery : IRequest<BlogListViewDto>
    {
        public int BlogId { get; set; }

        public class SuprisedBlogQueryHandler : IRequestHandler<SuprisedBlogQuery, BlogListViewDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public SuprisedBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<BlogListViewDto> Handle(SuprisedBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.BlogId);

                blog.ReactionSuprisedCount++;

                BlogListViewDto blogListViewDto = mapper.Map<BlogListViewDto>(blog);

                return blogListViewDto;
            }
        }
    }
}
