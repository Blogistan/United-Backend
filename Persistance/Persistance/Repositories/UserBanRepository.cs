using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class UserBanRepository : EfRepositoryBase<UserBan, Guid, EFDbContext>, IUserBanRepository
    {
        public UserBanRepository(EFDbContext context) : base(context)
        {
        }
    }
}
