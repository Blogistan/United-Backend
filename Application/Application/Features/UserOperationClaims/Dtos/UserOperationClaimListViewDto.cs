using Application.Features.OperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class UserOperationClaimListViewDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<OperationClaimListViewDto> Claims { get; set; }
    }
}
