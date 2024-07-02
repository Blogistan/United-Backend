using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken,int>, IRepository<RefreshToken,int>
    {
        
        Task<ICollection<RefreshToken>> GetAllOldActiveRefreshTokenAsync(UserBase user, int ttl);
    }
}
