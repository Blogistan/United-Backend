using Application.Features.Reports.Commands.CreateReport;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Reports.Commands.CreateReport.CreateReportCommand;

namespace AuthTest.Features.Reports.Commands.Create
{
    public class CreateReportTest : ReportMockRepository, IClassFixture<Startup>
    {
        private readonly CreateReportCommand _command;
        private readonly CreateReportCommandValidator _validator;
        private readonly CreateReportCommandHandler _handler;
        public CreateReportTest(ReportFakeData fakeData) : base(fakeData)
        {
            this._command = new CreateReportCommand();
            this._validator = new CreateReportCommandValidator();
            this._handler = new CreateReportCommandHandler(MockRepository.Object, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfUserIDEmpty()
        {
            this._command.ReportTypeID = 1;
            TestValidationResult<CreateReportCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserID);

        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeIDEmpty()
        {
            this._command.UserID = 1;
            TestValidationResult<CreateReportCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportTypeID);

        }
    }
}
