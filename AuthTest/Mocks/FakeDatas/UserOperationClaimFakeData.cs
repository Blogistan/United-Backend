using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class UserOperationClaimFakeData : BaseFakeData<UserOperationClaim, int>
    {
        public override List<UserOperationClaim> CreateFakeData()
        {
            return new List<UserOperationClaim>
            {
            new() { Id = 1,OperationClaimId=1,UserId=123 },
            new() { Id = 2,OperationClaimId=2,UserId=123 },
            new() { Id = 3,OperationClaimId=3,UserId=123 },

            };
        }
    }
}
