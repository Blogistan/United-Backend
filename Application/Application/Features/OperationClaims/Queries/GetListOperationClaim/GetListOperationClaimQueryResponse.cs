using Application.Features.OperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public record GetListOperationClaimQueryResponse :IResponse
    {
        public List<OperationClaimListViewDto> Items { get; set; }

        public GetListOperationClaimQueryResponse(List<OperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
