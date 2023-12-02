using Application.Features.OperationClaims.Dtos;
using MediatR;

namespace Application.Features.OperationClaims.Queries.NewFolder.GetListOperationClaim
{
    public class GetListOperationClaimQueryResponse
    {
        public List<OperationClaimListViewDto> Items { get; set; }
    }
}
