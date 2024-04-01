using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;

namespace AuthTest.Mocks.Repositories
{
    public class UserMockRepository : BaseMockRepository<ISiteUserRepository, SiteUser, int, MappingProfiles, AuthBussinessRules, SiteUserFakeData>
    {
        public UserMockRepository(SiteUserFakeData fakeData) : base(fakeData)
        {
        }
    }
}
