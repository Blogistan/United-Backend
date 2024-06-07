using Application.Features.Bans.Commands.CreateBan;
using Application.Features.Bans.Commands.DeleteBan;
using Application.Features.Bans.Profiles;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Bans.Commands.DeleteBan.DeleteBanCommand;

namespace AuthTest.Features.Auth.Bans.Commands.Delete
{
    public class DeleteBanTest : IClassFixture<Startup>
    {
        private readonly BanFakeData banFakeData;
        private readonly SiteUserFakeData siteUserFakeData;
        private readonly ReportFakeData reportFakeData;

        private readonly DeleteBanCommand deleteBanCommand;
        private readonly DeleteBanCommandHandler deleteBanCommandHandler;
        private readonly DeleteBanCommandValidator validationRules;

        public DeleteBanTest(BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        {
            this.banFakeData = banFakeData;
            this.siteUserFakeData = siteUserFakeData;
            this.reportFakeData = reportFakeData;

            IBanRepository banRepository = BanMockRepository.GetRepository(banFakeData, siteUserFakeData, reportFakeData).Object;
            ISiteUserRepository siteUserRepository = new UserMockRepository(siteUserFakeData).MockRepository.Object;
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));

            this.deleteBanCommand = new DeleteBanCommand();
            this.deleteBanCommandHandler = new DeleteBanCommandHandler(banRepository, mapper, siteUserRepository);
            this.validationRules = new DeleteBanCommandValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfReportIDIsEmpty()
        {
            TestValidationResult<DeleteBanCommand> testValidationResult = validationRules.TestValidate(deleteBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportID);
        }
    }
}
