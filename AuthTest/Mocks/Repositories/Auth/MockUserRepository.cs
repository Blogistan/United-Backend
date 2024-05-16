using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockUserRepository
    {

        private readonly SiteUserFakeData siteUserFakeData;
        private readonly BanFakeData banFakeData;
        public MockUserRepository(SiteUserFakeData siteUserFakeData, BanFakeData banFakeData)
        {
            this.siteUserFakeData = siteUserFakeData;
            this.banFakeData = banFakeData;
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

                    user.Bans = banFakeData.Data.Where(x => x.UserID == user.Id).ToList();

                    return user;
                });

            return mockRepo.Object;
        }

    }
}
