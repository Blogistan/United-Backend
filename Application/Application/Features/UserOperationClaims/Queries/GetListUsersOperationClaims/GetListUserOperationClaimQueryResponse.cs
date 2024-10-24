using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims
{
    public record GetListUserOperationClaimQueryResponse :IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public GetListUserOperationClaimQueryResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
