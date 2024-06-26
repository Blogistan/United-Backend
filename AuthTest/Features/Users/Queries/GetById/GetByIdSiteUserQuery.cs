﻿using Application.Features.SiteUsers.Queries.GetById;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using static Application.Features.SiteUsers.Queries.GetById.GetByIdSiteUserQuery;

namespace AuthTest.Features.Users.Queries.GetById
{
    public class GetByIdSiteUserQueryTest : UserMockRepository,IClassFixture<Startup>
    {
        private readonly GetByIdSiteUserQuery getByIdSiteUserQuery;
        private readonly GetByIdSiteUserQueryHandler getByIdSiteUserQueryHandler;
        public GetByIdSiteUserQueryTest(SiteUserFakeData siteUserFakeData, GetByIdSiteUserQuery getByIdSiteUserQuery) : base(siteUserFakeData)
        {
            this.getByIdSiteUserQuery = getByIdSiteUserQuery;
            this.getByIdSiteUserQueryHandler = new GetByIdSiteUserQueryHandler(MockRepository.Object, Mapper,BusinessRules);
        }
        [Fact]
        public async Task GetByIdUserShouldSuccessfully()
        {
            getByIdSiteUserQuery.Id = SiteUserFakeData.Ids[0];

            GetByIdSiteUserQueryResponse response = await getByIdSiteUserQueryHandler.Handle(getByIdSiteUserQuery, CancellationToken.None);

            Assert.Equal("example@united.io", response.SiteUserListViewDto.Email);
        }
        [Fact]
        public async Task UserIdNotExistsShouldReturnError()
        {
            getByIdSiteUserQuery.Id = SiteUserFakeData.Ids[0];

            async Task Action() => await getByIdSiteUserQueryHandler.Handle(getByIdSiteUserQuery, CancellationToken.None);

            await Assert.ThrowsAsync<BusinessException>(Action);
        }
    }
}
