using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic
{
    public record GetListUserOperationClaimDynamicQueryResponse :IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public GetListUserOperationClaimDynamicQueryResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
