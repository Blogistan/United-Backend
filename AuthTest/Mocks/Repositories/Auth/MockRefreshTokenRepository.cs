using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Core.Test.Application.FakeData;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockRefreshTokenRepository
    {

        private readonly RefreshTokenFakeData refreshTokenFakeData;
        private readonly SiteUserFakeData siteUserFakeData;
        public MockRefreshTokenRepository(RefreshTokenFakeData refreshTokenFakeData, SiteUserFakeData siteUserFakeData)
        {
            this.refreshTokenFakeData = refreshTokenFakeData;
            this.siteUserFakeData = siteUserFakeData;
        }

        public IRefreshTokenRepository GetRefreshTokenRepository()
        {
            List<RefreshToken> refreshTokens = refreshTokenFakeData.Data;
            var mockRepo = new Mock<IRefreshTokenRepository>();
            //mockRepo.Setup(x => x.GetAllOldActiveRefreshTokenAsync(It.IsAny<User>(), It.IsAny<int>())).ReturnsAsync(() => refreshTokens);

            mockRepo.Setup(s => s.GetAsync(
                    It.IsAny<Expression<Func<RefreshToken, bool>>>(),
                    It.IsAny<Func<IQueryable<RefreshToken>, IIncludableQueryable<RefreshToken, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<RefreshToken, bool>> predicate,
                    Func<IQueryable<RefreshToken>, IIncludableQueryable<RefreshToken, object>>? include,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {
                    RefreshToken refreshToken = new RefreshToken();

                    if (predicate != null)
                        refreshToken = refreshTokenFakeData.Data.Where(predicate.Compile()).FirstOrDefault();

                    if (refreshToken != null)
                        refreshToken.User = siteUserFakeData.Data.FirstOrDefault(x => x.Id == refreshToken.UserId);

                    return refreshToken;
                });

            return mockRepo.Object;
        }

    }
}
