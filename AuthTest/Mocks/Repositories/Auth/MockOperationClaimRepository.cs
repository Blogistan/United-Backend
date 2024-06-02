using Application.Features.OperationClaims.Profiles;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Core.Test.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockOperationClaimRepository : BaseMockRepository<IOperationClaimRepostiory, OperationClaim, int, MappingProfiles, OperationClaimBusinessRules, OperationClaimFakeData, IMediator>
    {
        private readonly OperationClaimFakeData operationClaimFakeData;
        public MockOperationClaimRepository(OperationClaimFakeData fakeData) : base(fakeData)
        {
            this.operationClaimFakeData = fakeData;
        }
        public IOperationClaimRepostiory GetOperationClaimRepostiory()
        {
            var mock = new Mock<IOperationClaimRepostiory>();

            mock.Setup(s => s.GetAsync(
                    It.IsAny<Expression<Func<OperationClaim, bool>>>(),
                    It.IsAny<Func<IQueryable<OperationClaim>, IIncludableQueryable<OperationClaim, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<OperationClaim, bool>> predicate,
                    Func<IQueryable<OperationClaim>, IIncludableQueryable<OperationClaim, object>>? include,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {
                    OperationClaim operationClaim = new OperationClaim();

                    if (predicate != null)
                        operationClaim = operationClaimFakeData.Data.FirstOrDefault(predicate.Compile());



                    return operationClaim;
                });

            return mock.Object;
        }
    }
}
