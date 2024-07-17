using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class UserLoginRepository : EfRepositoryBase<UserLogin, int, EFDbContext>, IUserLoginRepository
    {
        public UserLoginRepository(EFDbContext context) : base(context)
        {
        }
    }
}
