using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IUserRepository : IRepository<User,int>, IAsyncRepository<User,int>
    {
    }
}
