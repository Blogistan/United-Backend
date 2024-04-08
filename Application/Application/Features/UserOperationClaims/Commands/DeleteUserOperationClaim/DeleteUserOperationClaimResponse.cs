using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimResponse:IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public DeleteUserOperationClaimResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }

    }
}
