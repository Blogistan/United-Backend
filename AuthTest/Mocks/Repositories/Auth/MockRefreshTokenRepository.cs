using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Moq;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockRefreshTokenRepository
    {

        private readonly RefreshTokenFakeData refreshTokenFakeData;
        public MockRefreshTokenRepository(RefreshTokenFakeData refreshTokenFakeData)
        {
            this.refreshTokenFakeData = refreshTokenFakeData;
        }

        public IRefreshTokenRepository GetRefreshTokenRepository()
        {
            List<RefreshToken> refreshTokens = refreshTokenFakeData.Data;
            var mockRepo = new Mock<IRefreshTokenRepository>();
            mockRepo.Setup(x => x.GetAllOldActiveRefreshTokenAsync(It.IsAny<User>(), It.IsAny<int>())).ReturnsAsync(() => refreshTokens);

            return mockRepo.Object;
        }

    }
}
