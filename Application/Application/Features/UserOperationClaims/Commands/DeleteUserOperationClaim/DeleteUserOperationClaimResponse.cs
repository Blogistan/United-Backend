using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

    }
}
