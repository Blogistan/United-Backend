using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Blogs.Queries.DecreaseSuprisedBlog
{
    public class DecreaseSuprisedBlogQuery : IRequest<BlogListViewDto>
    {
        public int BlogId { get; set; }


        public class DecreaseSuprisedBlogQueryHandler : IRequestHandler<DecreaseSuprisedBlogQuery, BlogListViewDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public DecreaseSuprisedBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<BlogListViewDto> Handle(DecreaseSuprisedBlogQuery request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.BlogId);

                blog.ReactionSuprisedCount--;

                Blog updatedBlog = await blogRepository.UpdateAsync(blog);

                BlogListViewDto blogListViewDto = mapper.Map<BlogListViewDto>(updatedBlog);

                return blogListViewDto;
            }
        }
    }
}
