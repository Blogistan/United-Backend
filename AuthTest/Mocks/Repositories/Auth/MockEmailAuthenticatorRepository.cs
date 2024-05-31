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
    public class MockEmailAuthenticatorRepository : BaseMockRepository<IEmailAuthenticatorRepository, EmailAuthenticator, int, MappingProfiles, AuthBussinessRules, EmailAuthenticatorFakeData, IMediator>
    {
        public MockEmailAuthenticatorRepository(EmailAuthenticatorFakeData fakeData) : base(fakeData)
        {
        }

        public static IEmailAuthenticatorRepository GetEmailAuthenticatorRepositoryMock()
        {
            var mockRepo = new Mock<IEmailAuthenticatorRepository>();
            return mockRepo.Object;
        }
    }
}
