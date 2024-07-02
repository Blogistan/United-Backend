using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, int, EFDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<ICollection<RefreshToken>> GetAllOldActiveRefreshTokenAsync(UserBase user, int ttl)
        {
            return await Query().Where(x => x.UserId == user.Id &&
            x.Revoked == null && x.Expires > DateTime.UtcNow && x.CreatedDate.AddMinutes(ttl) < DateTime.UtcNow).ToListAsync();
        }
    }
}
