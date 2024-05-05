using Application.Features.SiteUsers.Queries.GetList;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.Application.Requests;
using static Application.Features.SiteUsers.Queries.GetList.GetListSiteUserQuery;

namespace AuthTest.Features.Users.Queries.GetList
{
    public class GetListSiteUserQueryTest : UserMockRepository, IClassFixture<Startup>
    {
        private readonly GetListSiteUserQuery getListSiteUserQuery;
        private readonly GetListSiteUserQueryHandler getListSiteUserQueryHandler;

        public GetListSiteUserQueryTest(SiteUserFakeData siteUserFakeData, GetListSiteUserQuery getListSiteUserQuery) : base(siteUserFakeData)
        {
            this.getListSiteUserQuery = getListSiteUserQuery;
            this.getListSiteUserQueryHandler = new GetListSiteUserQueryHandler(Mapper,MockRepository.Object);
        }
        [Fact]
        public async Task GetAllUsersShouldSuccessfuly()
        {
            getListSiteUserQuery.PageRequest = new PageRequest { Page = 0, PageSize = 3 };

            GetListSiteUserQueryResponse response = await getListSiteUserQueryHandler.Handle(getListSiteUserQuery, CancellationToken.None);

            Assert.NotEmpty(response.Items);
        }

    }
}
