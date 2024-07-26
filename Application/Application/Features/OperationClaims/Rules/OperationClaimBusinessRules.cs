using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules: BaseBusinessRules
    {
        private readonly IOperationClaimRepostiory operationClaimRepostiory;
        public OperationClaimBusinessRules(IOperationClaimRepostiory operationClaimRepostiory)
        {
            this.operationClaimRepostiory = operationClaimRepostiory;
        }
        public async Task OperationClaimCannotBeDuplicatedWhenInserted(string name)
        {
            OperationClaim operationClaim = await operationClaimRepostiory.GetAsync(x => x.Name == name);
            if (operationClaim is not null)
                throw new ValidationException("Operation Claim is exists.");
        }
        public async Task BlogCannotBeDuplicatedWhenUpdated(string name)
        {
            OperationClaim operationClaim = await operationClaimRepostiory.GetAsync(x => x.Name == name);
            if (operationClaim is not null)
                throw new ValidationException("Operation Claim is exists.");
        }
        public async Task<OperationClaim> OperationClaimCheckById(int id)
        {
            OperationClaim operationClaim = await operationClaimRepostiory.GetAsync(x => x.Id == id);
            if (operationClaim == null) throw new NotFoundException("Operation Claim is not exists.");

            return operationClaim;
        }
    }
}
