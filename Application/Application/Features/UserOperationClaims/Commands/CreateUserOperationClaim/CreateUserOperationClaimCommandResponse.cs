using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommandResponse
    {
        public int UserId { get; set; }
        public string OperationClaimId { get; set; }
        public List<OperationClaim> Claims { get; set; }
    }
}
