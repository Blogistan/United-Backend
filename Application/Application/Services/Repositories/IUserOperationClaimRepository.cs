using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IUserOperationClaimRepository : IRepository<UserOperationClaim,int>, IAsyncRepository<UserOperationClaim,int>
    {
        Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId);
    }
}
