using Application.Features.SiteUsers.Queries.GetList;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.Application.Requests;
using static Application.Features.SiteUsers.Queries.GetList.GetListSiteUserQuery;

namespace AuthTest.Features.Users.Queries.GetList
{
    public class GetListSiteUserQueryTest : UserMockRepository
    {
        private readonly GetListSiteUserQuery getListSiteUserQuery;
        private readonly GetListSiteUserQueryHandler getListSiteUserQueryHandler;

        public GetListSiteUserQueryTest(SiteUserFakeData siteUserFakeData, GetListSiteUserQuery getListSiteUserQuery, GetListSiteUserQueryHandler getListSiteUserQueryHandler) : base(siteUserFakeData)
        {
            this.getListSiteUserQuery = getListSiteUserQuery;
            this.getListSiteUserQueryHandler = getListSiteUserQueryHandler;
        }
        [Fact]
        public async Task GetAllUsersShouldSuccessfuly()
        {
            getListSiteUserQuery.PageRequest = new PageRequest { Page = 0, PageSize = 3 };

            GetListSiteUserQueryResponse response = await getListSiteUserQueryHandler.Handle(getListSiteUserQuery, CancellationToken.None);

            Assert.Equal(3, response.Items.Count);
        }

    }
}
