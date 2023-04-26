using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int,EFDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(EFDbContext context) : base(context)
        {
        }
    }
}
