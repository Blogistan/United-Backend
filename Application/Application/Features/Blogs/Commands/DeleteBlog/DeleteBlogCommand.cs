using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Blogs.Commands.DeleteBlog
{
    public class DeleteBlogCommand:IRequest<DeleteBlogCommandResponse>
    {
        public int Id { get; set; }

        public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, DeleteBlogCommandResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly BlogBusinessRules blogBusinessRules;
            private readonly IMapper mapper;

            public DeleteBlogCommandHandler(IBlogRepository blogRepository, BlogBusinessRules blogBusinessRules, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.blogBusinessRules = blogBusinessRules;
                this.mapper = mapper;
            }
            public async Task<DeleteBlogCommandResponse> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
            {
                var blog = await blogBusinessRules.BlogCheckById(request.Id);

                Blog deletedBlog = await blogRepository.DeleteAsync(blog,true);

                DeleteBlogCommandResponse response = mapper.Map<DeleteBlogCommandResponse>(deletedBlog);

                return response;
            }
        }
    }
}
