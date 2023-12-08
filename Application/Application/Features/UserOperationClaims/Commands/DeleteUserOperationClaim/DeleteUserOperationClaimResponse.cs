using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<OperationClaim> Claims { get; set; }

    }
}
