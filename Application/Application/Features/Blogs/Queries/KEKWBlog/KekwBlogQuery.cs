using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Queries.LikeBlog;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blogs.Queries.KEKWBlog
{
    public class KekwBlogQuery:IRequest<BlogListViewDto>
    {
        public int BlogId { get; set; }

        public class KekwBlogQueryHandler : IRequestHandler<KekwBlogQuery, BlogListViewDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public KekwBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<BlogListViewDto> Handle(KekwBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.BlogId);

                blog.ReactionKEKWCount++;

                Blog updatedBlog = await blogRepository.UpdateAsync(blog);

                BlogListViewDto blogListViewDto = mapper.Map<BlogListViewDto>(updatedBlog);

                return blogListViewDto;
            }
        }
    }
}
