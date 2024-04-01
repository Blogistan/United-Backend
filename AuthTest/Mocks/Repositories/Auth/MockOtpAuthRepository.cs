using Application.Services.Repositories;
using Moq;

namespace AuthTest.Mocks.Repositories.Auth
{
    public static class MockOtpAuthRepository
    {
        public static IOtpAuthenticatorRepository GetOtpAuthenticatorRepository()
        {
            var mockRepo = new Mock<IOtpAuthenticatorRepository>();
            return mockRepo.Object;
        }
    }
}
