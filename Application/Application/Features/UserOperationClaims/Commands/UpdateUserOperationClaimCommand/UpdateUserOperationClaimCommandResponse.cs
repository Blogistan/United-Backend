using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommandResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }
    }
}
