using Application.Features.OperationClaims.Dtos;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQueryResponse:IResponse
    {
        public List<OperationClaimListViewDto> Items { get; set; }

        public GetListOperationClaimQueryResponse(List<OperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
