using Application.Features.UserOperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommandResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }
    }
}
