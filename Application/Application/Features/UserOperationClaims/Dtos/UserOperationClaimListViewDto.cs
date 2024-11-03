using Application.Features.OperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Dtos
{
    public record UserOperationClaimListViewDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }
        public List<OperationClaimListViewDto> Claims { get; set; }
    }
}
