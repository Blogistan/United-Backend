using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Rules;
using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Queries.BlogDetailById
{
    public class BlogDetailByIdQuery : IRequest<BlogDetailDto>
    {
        public int BlogId { get; set; }


        public class BlogDetailByIdQueryHandler : IRequestHandler<BlogDetailByIdQuery, BlogDetailDto>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            public BlogBusinessRules blogBusinessRules { get; set; }
            public BlogDetailByIdQueryHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules businessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = businessRules;
            }

            public async Task<BlogDetailDto> Handle(BlogDetailByIdQuery request, CancellationToken cancellationToken)
            {
                await blogBusinessRules.BlogCheckById(request.BlogId);

                var blog = await blogRepository.GetAsync(x => x.Id == request.BlogId, x => x.Include(x => x.Writer).Include(x => x.Content).Include(x => x.Category).ThenInclude(x => x.SubCategories));

                BlogDetailDto blogDetailDto = new()
                {
                    Id = blog.Id,
                    Category = mapper.Map<CategoryViewDto>(blog.Category),
                    CreatedDate = blog.CreatedDate,
                    Title = blog.Title,
                    WriterName = blog.Writer.FirstName + " " + blog.Writer.LastName,
                    ContentImageUrl = blog.Content.ContentImageUrl,
                    ContentPragraph = blog.Content.ContentPragraph,
                    ReactionKEKWCount = blog.ReactionKEKWCount,
                    ReactionLovelyCount = blog.ReactionLovelyCount,
                    ReactionSadCount = blog.ReactionSadCount,
                    ReactionSuprisedCount = blog.ReactionSuprisedCount,
                    ReactionTriggeredCount = blog.ReactionTriggeredCount,
                    ReadCount = blog.ReadCount,
                    ShareCount = blog.ShareCount,
                    TitleContent = blog.Content.Title
                };

                return blogDetailDto;

            }
        }

    }
}
