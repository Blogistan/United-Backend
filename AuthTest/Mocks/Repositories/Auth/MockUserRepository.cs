using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockUserRepository
    {

        private readonly SiteUserFakeData siteUserFakeData;
        public MockUserRepository(SiteUserFakeData siteUserFakeData)
        {
            this.siteUserFakeData = siteUserFakeData;
        }
        public ISiteUserRepository GetSiteUserRepository()
        {
            var mockRepo = new Mock<ISiteUserRepository>();

            mockRepo.Setup(s => s.GetAsync(
                    It.IsAny<Expression<Func<SiteUser, bool>>>(),
                    It.IsAny<Func<IQueryable<SiteUser>, IIncludableQueryable<SiteUser, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<SiteUser, bool>> predicate,
                    Func<IQueryable<SiteUser>, IIncludableQueryable<SiteUser, object>>? include,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {
                    SiteUser? user = null;
                    if (predicate != null)
                        user = siteUserFakeData.Data.Where(predicate.Compile()).FirstOrDefault();
                    return user;
                });

            return mockRepo.Object;
        }

    }
}
