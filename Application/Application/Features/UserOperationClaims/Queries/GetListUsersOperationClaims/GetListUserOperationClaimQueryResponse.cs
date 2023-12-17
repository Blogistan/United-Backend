using Application.Features.UserOperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims
{
    public class GetListUserOperationClaimQueryResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }
    }
}
