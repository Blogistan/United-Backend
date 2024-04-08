using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic
{
    public class GetListUserOperationClaimDynamicQueryResponse:IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public GetListUserOperationClaimDynamicQueryResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
