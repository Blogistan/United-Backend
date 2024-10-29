using Application.Features.Bans.Commands.CreateBan;
using Application.Features.Bans.Profiles;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using System.Security.Cryptography;
using static Application.Features.Bans.Commands.CreateBan.CreateBanCommand;

namespace AuthTest.Features.Auth.Bans.Commands.Create
{
    public class CreateBanTest : IClassFixture<Startup>
    {
        private readonly BanFakeData banFakeData;
        private readonly SiteUserFakeData siteUserFakeData;
        private readonly ReportFakeData reportFakeData;

        private readonly CreateBanCommand createBanCommand;
        private readonly CreateBanCommandHandler createBanCommandHandler;
        private readonly CreateBanCommandValidator validationRules;

        public CreateBanTest(BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData,IRefreshTokenRepository refreshTokenRepository)
        {
            this.banFakeData = banFakeData;
            this.siteUserFakeData = siteUserFakeData;
            this.reportFakeData = reportFakeData;

            IBanRepository banRepository = BanMockRepository.GetRepository(banFakeData, siteUserFakeData, reportFakeData).Object;
            ISiteUserRepository siteUserRepository = new UserMockRepository(siteUserFakeData).MockRepository.Object;
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));

            this.createBanCommand = new CreateBanCommand();
            this.createBanCommandHandler = new CreateBanCommandHandler(banRepository, mapper, siteUserRepository, refreshTokenRepository);
            this.validationRules = new CreateBanCommandValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfUserIDIsEmpty()
        {
            createBanCommand.BanStartDate = DateTime.UtcNow;
            createBanCommand.BanEndDate = DateTime.UtcNow.AddDays(14);
            createBanCommand.IsPerma = false;
            createBanCommand.ReportID = RandomNumberGenerator.GetInt32(10);

            TestValidationResult<CreateBanCommand> testValidationResult = validationRules.TestValidate(createBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.SiteUserId);
        }
        [Fact]
        public async Task ThrowExceptionIfBanStartDateIsEmpty()
        {
            createBanCommand.SiteUserId = 1;
            createBanCommand.BanEndDate = DateTime.UtcNow.AddDays(14);
            createBanCommand.IsPerma = false;
            createBanCommand.ReportID = RandomNumberGenerator.GetInt32(10);
            createBanCommand.BanDetail = "TEST";

            TestValidationResult<CreateBanCommand> testValidationResult = validationRules.TestValidate(createBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.BanStartDate);
        }
        [Fact]
        public async Task ThrowExceptionIfBanStartEndIsEmpty()
        {
            createBanCommand.SiteUserId = 1;
            createBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            createBanCommand.IsPerma = false;
            createBanCommand.ReportID = RandomNumberGenerator.GetInt32(10);
            createBanCommand.BanDetail = "TEST";

            TestValidationResult<CreateBanCommand> testValidationResult = validationRules.TestValidate(createBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.BanEndDate);
        }
        [Fact]
        public async Task ThrowExceptionIfPermaIsEmpty()
        {
            createBanCommand.SiteUserId = 1;
            createBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            createBanCommand.BanEndDate = DateTime.UtcNow.AddDays(19);
            createBanCommand.ReportID = RandomNumberGenerator.GetInt32(10);
            createBanCommand.BanDetail = "TEST";

            TestValidationResult<CreateBanCommand> testValidationResult = validationRules.TestValidate(createBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.IsPerma);
        }
        [Fact]
        public async Task ThrowExceptionIfReportIDIsEmpty()
        {
            createBanCommand.SiteUserId = 1;
            createBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            createBanCommand.BanEndDate = DateTime.UtcNow.AddDays(19);
            createBanCommand.IsPerma = false;
            createBanCommand.BanDetail = "TEST";


            TestValidationResult<CreateBanCommand> testValidationResult = validationRules.TestValidate(createBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportID);
        }
    }
}
