using Application.Features.UserOperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommandResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }
    }
}
