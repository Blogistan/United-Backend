using Application.Features.UserOperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaims
{
    public class GetListUserOperationClaimQueryResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }
    }
}
