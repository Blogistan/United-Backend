using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository userOperationClaimRepository;
        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            this.userOperationClaimRepository = userOperationClaimRepository;
        }
        public async Task UserOperationClaimCannotBeDuplicatedWhenInserted(int userID,int claim)
        {
            UserOperationClaim userOperationClaim = await userOperationClaimRepository.GetAsync(x => x.OperationClaimId == claim&&x.UserId==userID);
            if (userOperationClaim is not null)
                throw new BusinessException("User Operation Claim is exists.");
        }
        public async Task UserOperationClaimCannotBeDuplicatedWhenUpdated(int userID, int claim)
        {
            UserOperationClaim userOperationClaim = await userOperationClaimRepository.GetAsync(x => x.OperationClaimId == claim && x.UserId == userID);
            if (userOperationClaim is not null)
                throw new BusinessException("User Operation Claim is exist");
        }
        public async Task<UserOperationClaim> UserOperationClaimCheckById(int userID, int claim)
        {
            UserOperationClaim userOperationClaim = await userOperationClaimRepository.GetAsync(x => x.OperationClaimId == claim && x.UserId == userID);
            if (userOperationClaim == null) throw new BusinessException("User Operation Claim is not exists.");

            return userOperationClaim;
        }
    }
}
