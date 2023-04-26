using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IBlogRepository : IAsyncRepository<Blog,int>, IRepository<Blog
        ,int>
    {
    }
}
