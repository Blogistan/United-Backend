using Application.Features.ReportTypes.Commands.DeleteReportType;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.ReportTypes.Commands.DeleteReportType.DeleteReportTypeCommand;

namespace AuthTest.Features.ReportTypes.Command.Delete
{
    public class DeleteReportTypeTest : ReportTypeMockRepository, IClassFixture<Startup>
    {
        private readonly DeleteReportTypeCommand _command;
        private readonly DeleteReportTypeCommandHandler _handler;
        private readonly DeleteReportTypeCommandValidator _validator;

        public DeleteReportTypeTest(ReportTypeFakeData fakeData) : base(fakeData)
        {
            this._command = new DeleteReportTypeCommand();
            this._validator = new DeleteReportTypeCommandValidator();
            this._handler = new DeleteReportTypeCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfIdIfEmpty()
        {
            TestValidationResult<DeleteReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeNotExists()
        {
            _command.Id = 5641;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
    }
}
