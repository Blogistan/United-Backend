using Application.Features.Auth.Profiles;
using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class UserMockRepository : BaseMockRepository<ISiteUserRepository, SiteUser, int, MappingProfiles, UserBusinessRules, SiteUserFakeData,IMediator>
    {
        public UserMockRepository(SiteUserFakeData fakeData) : base(fakeData)
        {
        }
    }
}
