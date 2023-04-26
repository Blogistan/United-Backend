using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, int,EFDbContext>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(EFDbContext context) : base(context)
        {
        }
    }
}
