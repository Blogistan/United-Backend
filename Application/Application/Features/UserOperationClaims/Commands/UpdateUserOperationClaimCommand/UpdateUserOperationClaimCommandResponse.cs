using Application.Features.OperationClaims.Dtos;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommandResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<OperationClaimListViewDto> Claims { get; set; }
    }
}
