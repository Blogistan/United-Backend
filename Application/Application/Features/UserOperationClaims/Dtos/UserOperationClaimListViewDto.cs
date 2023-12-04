using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class UserOperationClaimListViewDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<OperationClaim> Claims { get; set; }
    }
}
