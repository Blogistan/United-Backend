using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, EFDbContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId)
        {
            List<OperationClaim> operationClaims = await Query()
                .AsNoTracking()
                .Where(x => x.UserId.Equals(userId))
                .Select(o => new OperationClaim { Id = o.OperationClaim.Id, Name = o.OperationClaim.Name })
                .ToListAsync();

            return operationClaims;
        }
    }
}
