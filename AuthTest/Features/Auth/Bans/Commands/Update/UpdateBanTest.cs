using Application.Features.Bans.Commands.UpdateBan;
using Application.Features.Bans.Commands.UpdateBan;
using Application.Features.Bans.Profiles;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Bans.Commands.UpdateBan.UpdateBanCommand;

namespace AuthTest.Features.Auth.Bans.Commands.Update
{
    public class UpdateBanTest:IClassFixture<Startup>
    {
        private readonly BanFakeData banFakeData;
        private readonly SiteUserFakeData siteUserFakeData;
        private readonly ReportFakeData reportFakeData;

        private readonly UpdateBanCommand updateBanCommand;
        private readonly UpdateBanCommandHandler updateBanCommandHandler;
        private readonly UpdateBanCommandValidator validationRules;

        public UpdateBanTest(BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        {
            this.banFakeData = banFakeData;
            this.siteUserFakeData = siteUserFakeData;
            this.reportFakeData = reportFakeData;

            IBanRepository banRepository = BanMockRepository.GetRepository(banFakeData, siteUserFakeData, reportFakeData).Object;
            ISiteUserRepository siteUserRepository = new UserMockRepository(siteUserFakeData).MockRepository.Object;
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));

            this.updateBanCommand = new UpdateBanCommand();
            this.updateBanCommandHandler = new UpdateBanCommandHandler(banRepository, mapper, siteUserRepository);
            this.validationRules = new UpdateBanCommandValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfUserIDIsEmpty()
        {
            updateBanCommand.BanStartDate = DateTime.UtcNow;
            updateBanCommand.BanEndDate = DateTime.UtcNow.AddDays(14);
            updateBanCommand.IsPerma = false;
            updateBanCommand.ReportID = Guid.NewGuid();

            TestValidationResult<UpdateBanCommand> testValidationResult = validationRules.TestValidate(updateBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserId);
        }
        [Fact]
        public async Task ThrowExceptionIfBanStartDateIsEmpty()
        {
            updateBanCommand.UserId = 1;
            updateBanCommand.BanEndDate = DateTime.UtcNow.AddDays(14);
            updateBanCommand.IsPerma = false;
            updateBanCommand.ReportID = Guid.NewGuid();
            updateBanCommand.BanDetail = "TEST";

            TestValidationResult<UpdateBanCommand> testValidationResult = validationRules.TestValidate(updateBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.BanStartDate);
        }
        [Fact]
        public async Task ThrowExceptionIfBanStartEndIsEmpty()
        {
            updateBanCommand.UserId = 1;
            updateBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            updateBanCommand.IsPerma = false;
            updateBanCommand.ReportID = Guid.NewGuid();
            updateBanCommand.BanDetail = "TEST";

            TestValidationResult<UpdateBanCommand> testValidationResult = validationRules.TestValidate(updateBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.BanEndDate);
        }
        [Fact]
        public async Task ThrowExceptionIfPermaIsEmpty()
        {
            updateBanCommand.UserId = 1;
            updateBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            updateBanCommand.BanEndDate = DateTime.UtcNow.AddDays(19);
            updateBanCommand.ReportID = Guid.NewGuid();
            updateBanCommand.BanDetail = "TEST";

            TestValidationResult<UpdateBanCommand> testValidationResult = validationRules.TestValidate(updateBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.IsPerma);
        }
        [Fact]
        public async Task ThrowExceptionIfReportIDIsEmpty()
        {
            updateBanCommand.Id = Guid.NewGuid();
            updateBanCommand.UserId = 1;
            updateBanCommand.BanStartDate = DateTime.UtcNow.AddDays(14);
            updateBanCommand.BanEndDate = DateTime.UtcNow.AddDays(19);
            updateBanCommand.IsPerma = false;
            updateBanCommand.BanDetail = "TEST";


            TestValidationResult<UpdateBanCommand> testValidationResult = validationRules.TestValidate(updateBanCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportID);
        }
    }
}
