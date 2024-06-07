using Application.Features.Bans.Profiles;
using Application.Features.Bans.Queries.GetListBans;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Bans.Queries.GetListBans.GetListBansQuery;

namespace AuthTest.Features.Auth.Bans.Queries.GetList
{
    public class GetListBanTest : IClassFixture<Startup>
    {
        private readonly BanFakeData banFakeData;
        private readonly SiteUserFakeData siteUserFakeData;
        private readonly ReportFakeData reportFakeData;

        private readonly GetListBansQuery getListBansQuery;
        private readonly GetListBansQueryHandler getListBansQueryHandler;
        private readonly GetListQueryValidator validationRules;

        public GetListBanTest(BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        {
            this.banFakeData = banFakeData;
            this.siteUserFakeData = siteUserFakeData;
            this.reportFakeData = reportFakeData;

            IBanRepository banRepository = BanMockRepository.GetRepository(banFakeData, siteUserFakeData, reportFakeData).Object;
            ISiteUserRepository siteUserRepository = new UserMockRepository(siteUserFakeData).MockRepository.Object;
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));

            this.getListBansQuery = new GetListBansQuery();
            this.getListBansQueryHandler = new GetListBansQueryHandler(banRepository, mapper);
            this.validationRules = new GetListQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            TestValidationResult<GetListBansQuery> testValidationResult = validationRules.TestValidate(getListBansQuery);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task GetDataSuccessfully()
        {
            getListBansQuery.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };

            var response = await getListBansQueryHandler.Handle(getListBansQuery, CancellationToken.None);

            Assert.NotEmpty(response.Items);
        }

    }
}
