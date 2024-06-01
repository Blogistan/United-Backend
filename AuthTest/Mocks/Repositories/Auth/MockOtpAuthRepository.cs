using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Core.Test.Application.Repositories;
using MediatR;
using Moq;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockOtpAuthRepository : BaseMockRepository<IOtpAuthenticatorRepository, OtpAuthenticator, int, MappingProfiles, AuthBussinessRules, OtpAuthenticatorFakeData, IMediator>
    {
        public MockOtpAuthRepository(OtpAuthenticatorFakeData fakeData) : base(fakeData)
        {
        }
        public static IOtpAuthenticatorRepository GetOtpAuthenticatorRepository()
        {
            var mockRepo = new Mock<IOtpAuthenticatorRepository>();
            return mockRepo.Object;
        }
    }
}
