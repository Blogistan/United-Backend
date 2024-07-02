using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IUserRepository : IRepository<UserBase,int>, IAsyncRepository<UserBase,int>
    {
    }
}
