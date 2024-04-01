using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Moq;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockUserOperationClaimRepository
    {
        private readonly OperationClaimFakeData operationClaimFakeData;
        public MockUserOperationClaimRepository(OperationClaimFakeData operationClaimFakeData)
        {
            this.operationClaimFakeData = operationClaimFakeData;
        }

        public IOperationClaimRepostiory GetOperationClaimRepostiory()
        {
            List<OperationClaim> operationClaims = operationClaimFakeData.Data;

            var mockRepo = new Mock<IOperationClaimRepostiory>();

            //GetOperationClaimsByUserIdAsync will created.
            //mockRepo
            //.Setup(s => s.GetOperationClaimsByUserIdAsync(It.IsAny<Guid>()))
            //.ReturnsAsync(
            //    (Guid userId) =>
            //    {
            //        var claims = operationClaims.ToList();
            //        return claims;
            //    }
            //);

            return mockRepo.Object;

        }
    }
}
