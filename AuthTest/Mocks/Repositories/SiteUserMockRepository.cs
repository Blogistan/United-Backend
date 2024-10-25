using Application.Features.Auth.Profiles;
using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Core.Test.Application.Repositories;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class SiteUserMockRepository : BaseMockRepository<IUserRepository, User, int, MappingProfiles, UserBusinessRules, UserFakeData, IMediator>
    {
        public SiteUserMockRepository(UserFakeData fakeData) : base(fakeData)
        {
        }
    }
}
