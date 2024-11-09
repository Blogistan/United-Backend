using Application.Features.OperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaimDynamic
{
    public record GetListOperationClaimDynamicQueryResponse : IResponse
    {
        public List<OperationClaimListViewDto> Items { get; set; }

        public GetListOperationClaimDynamicQueryResponse(List<OperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
