using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommand:IRequest<UpdateBlogCommandResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string BannerImageUrl { get; set; }
        public int WriterId { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }

        public int ShareCount { get; set; }
        public int ReadCount { get; set; }

        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "User" };

        public class UpdateBlogCommandHandler:IRequestHandler<UpdateBlogCommand, UpdateBlogCommandResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly BlogBusinessRules blogBusinessRules;
            private readonly IMapper mapper;
            public UpdateBlogCommandHandler(IBlogRepository blogRepository, BlogBusinessRules blogBusinessRules, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.blogBusinessRules = blogBusinessRules;
                this.mapper = mapper;
            }

            public async Task<UpdateBlogCommandResponse> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
            {
                //await blogBusinessRules.BlogCannotBeDuplicatedWhenUpdated(request.Title);

                Blog blog = mapper.Map<Blog>(request);

                Blog updatedBlog = await blogRepository.UpdateAsync(blog);

                UpdateBlogCommandResponse response = mapper.Map<UpdateBlogCommandResponse>(updatedBlog);

                return response;

            }
        }
    }
}
