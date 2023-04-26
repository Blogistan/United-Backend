using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class OperationClaimRepository : EfRepositoryBase<OperationClaim, int,EFDbContext>, IOperationClaimRepostiory
    {
        public OperationClaimRepository(EFDbContext context) : base(context)
        {
        }
    }
}
