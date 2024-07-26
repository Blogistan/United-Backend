using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Blogs.Rules
{
    public class BlogBusinessRules:BaseBusinessRules
    {

        private readonly IBlogRepository blogRepository;
        public BlogBusinessRules(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        public async Task BlogCannotBeDuplicatedWhenInserted(string title)
        {
            Blog blog = await blogRepository.GetAsync(x => x.Title==title);
            if (blog is not null)
                throw new ValidationException("Blog is exists.");
        }
        public async Task BlogCannotBeDuplicatedWhenUpdated(string title)
        {
            Blog blog = await blogRepository.GetAsync(x => x.Title == title);
            if (blog is not null)
                throw new ValidationException("Blog is exist");
        }
        public async Task<Blog> BlogCheckById(int id)
        {
            Blog blog = await blogRepository.GetAsync(x => x.Id == id);
            if (blog == null) throw new NotFoundException("Blog is not exists.");

            return blog;
        }
    }
}
