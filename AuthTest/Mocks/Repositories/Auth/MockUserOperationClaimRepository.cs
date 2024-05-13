using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockUserOperationClaimRepository
    {
        private readonly UserOperationClaimFakeData userOperationClaimFakeData;
        public MockUserOperationClaimRepository(UserOperationClaimFakeData userOperationClaimFakeData)
        {
            this.userOperationClaimFakeData = userOperationClaimFakeData;
        }

        public IUserOperationClaimRepository GetOperationClaimRepostiory()
        {
            List<UserOperationClaim> userOperationClaims = userOperationClaimFakeData.Data;

            var mockRepo = new Mock<IUserOperationClaimRepository>();


            mockRepo.Setup(s => s.GetListAsync(
            It.IsAny<Expression<Func<UserOperationClaim, bool>>>(),
            It.IsAny<Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>>(),
            It.IsAny<Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<UserOperationClaim, bool>>? predicate,
                    Func<IQueryable<UserOperationClaim>, IOrderedQueryable<UserOperationClaim>>? orderBy,
                    Func<IQueryable<UserOperationClaim>, IIncludableQueryable<UserOperationClaim, object>>? include,
                    int index,
                    int size,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {

                    //IPaginate<UserOperationClaim>? operationClaims = new Paginate<UserOperationClaim>();
                    //if (predicate != null)
                    //    operationClaims.Items.Add((UserOperationClaim)userOperationClaimFakeData.Data.Where(predicate.Compile()).Select(oc=>new UserOperationClaim { Id=oc.Id,OperationClaimId=oc.OperationClaimId}));
                    //return  operationClaims;

                    var query = userOperationClaimFakeData.Data.AsQueryable();
                    if (include != null)
                    {
                        query = include(query);
                    }
                    
                    IPaginate<UserOperationClaim>? operationClaims = new Paginate<UserOperationClaim>
                    {
                        Items = query.ToList()
                    };
                    return operationClaims;
                    
                });

            //mockRepo
            //.Setup(s => s.GetOperationClaimsByUserIdAsync(It.IsAny<int>()))
            //.Returns(async (int userId) =>
            //{
            //    var claims = userOperationClaims.ToList();
            //    return await Task.FromResult((IList<OperationClaim>)claims);
            //});

            return mockRepo.Object;

        }
    }
}
