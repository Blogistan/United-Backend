using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Blogs.Queries.ShareBlog
{
    public class ShareBlogQuery : IRequest<BlogListViewDto>
    {
        public int BlogId { get; set; }
        public class ShareBlogQueryHandler : IRequestHandler<ShareBlogQuery, BlogListViewDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public ShareBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<BlogListViewDto> Handle(ShareBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.BlogId);

                blog.ShareCount++;

                Blog updatedBlog = await blogRepository.UpdateAsync(blog);

                BlogListViewDto blogListViewDto = mapper.Map<BlogListViewDto>(updatedBlog);

                return blogListViewDto;
            }
        }
    }
}
