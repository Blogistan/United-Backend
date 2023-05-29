using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Blogs.Queries.LikeBlog
{
    public class ReadBlogQuery:IRequest<BlogListViewDto>
    {
        public int BlogId { get; set; }

        public class LikeBlogQueryHandler:IRequestHandler<ReadBlogQuery, BlogListViewDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public LikeBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<BlogListViewDto> Handle(ReadBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.BlogId);

                blog.ReadCount++;

                BlogListViewDto blogListViewDto = mapper.Map<BlogListViewDto>(blog);

                return blogListViewDto;
            }
        }
    }
}
