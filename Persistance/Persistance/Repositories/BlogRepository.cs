using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class BlogRepository : EfRepositoryBase<Blog,int ,EFDbContext>, IBlogRepository
    {
        public BlogRepository(EFDbContext context) : base(context)
        {
        }
    }
}
