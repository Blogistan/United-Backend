using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<CreateBlogCommandResponse>
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string BannerImageUrl { get; set; }
        public int WriterId { get; set; }
        public int ReactionSuprisedCount => 0;
        public int ReactionLovelyCount => 0;
        public int ReactionSadCount => 0;
        public int ReactionKEKWCount => 0;
        public int ReactionTriggeredCount => 0;

        public int ShareCount => 0;
        public int ReadCount => 0;

        public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogCommandResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            public CreateBlogCommandHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
            }

            public async Task<CreateBlogCommandResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                await blogBusinessRules.BlogCannotBeDuplicatedWhenInserted(request.Title);

                Blog blog = mapper.Map<Blog>(request);

                Blog createdBlog = await blogRepository.AddAsync(blog);

                Blog blogReturn = await blogRepository.GetAsync(x=>x.Id==createdBlog.Id,include:inc=>inc.Include(x=>x.Writer).Include(x=>x.Category));

                CreateBlogCommandResponse response = mapper.Map<CreateBlogCommandResponse>(blogReturn);
                return response;
            }
        }
    }
}
