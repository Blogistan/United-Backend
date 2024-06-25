using Application.Features.Reports.Commands.UpdateReport;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Reports.Commands.UpdateReport.UpdateReportCommand;

namespace AuthTest.Features.Reports.Commands.Update
{
    public class UpdateReportTest : ReportMockRepository, IClassFixture<Startup>
    {
        private readonly UpdateReportCommand _command;
        private readonly UpdateReportCommandValidator _validator;
        private readonly UpdateReportCommandHandler _handler;
        public UpdateReportTest(ReportFakeData fakeData) : base(fakeData)
        {
            this._command = new UpdateReportCommand();
            this._validator = new UpdateReportCommandValidator();
            this._handler = new UpdateReportCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfReportIDEmpty()
        {
            TestValidationResult<UpdateReportCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportID);

        }
        [Fact]
        public async Task ThrowExceptionIfReportNotExists()
        {
            this._command.ReportID = Guid.NewGuid();
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
    }
}
