using Application.Features.Reports.Commands.DeleteReport;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using System.Security.Cryptography;
using static Application.Features.Reports.Commands.DeleteReport.DeleteReportCommand;

namespace AuthTest.Features.Reports.Commands.Delete
{
    public class DeleteReportTest : ReportMockRepository, IClassFixture<Startup>
    {
        private readonly DeleteReportCommand _command;
        private readonly DeleteReportCommandValidator _validator;
        private readonly DeleteReportCommandHandler _handler;
        public DeleteReportTest(ReportFakeData fakeData) : base(fakeData)
        {
            this._command = new DeleteReportCommand();
            this._validator = new DeleteReportCommandValidator();
            this._handler = new DeleteReportCommandHandler(MockRepository.Object, BusinessRules, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfReportIDEmpty()
        {
            TestValidationResult<DeleteReportCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportID);

        }
        [Fact]
        public async Task ThrowExceptionIfReportNotExists()
        {
            this._command.ReportID =RandomNumberGenerator.GetInt32(10);
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
    }
}
