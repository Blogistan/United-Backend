using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class UserRepository : EfRepositoryBase<User, int,EFDbContext>, IUserRepository
    {
        public UserRepository(EFDbContext context) : base(context)
        {
        }
    }
}
