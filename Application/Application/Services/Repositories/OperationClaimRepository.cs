using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IOperationClaimRepostiory : IRepository<OperationClaim,int>, IAsyncRepository<OperationClaim,int>
    {
    }
}
