using Application.Features.OperationClaims.Dtos;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim
{
    public class GetListOperationClaimQueryResponse
    {
        public List<OperationClaimListViewDto> Items { get; set; }
    }
}
