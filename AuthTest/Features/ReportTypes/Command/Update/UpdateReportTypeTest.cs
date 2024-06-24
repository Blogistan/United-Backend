using Application.Features.ReportTypes.Commands.UpdateReportType;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.ReportTypes.Commands.UpdateReportType.UpdateReportTypeCommand;

namespace AuthTest.Features.ReportTypes.Command.Update
{
    public class UpdateReportTypeTest : ReportTypeMockRepository, IClassFixture<Startup>
    {
        private readonly UpdateReportTypeCommand _command;
        private readonly UpdateReportTypeCommandHandler _handler;
        private readonly UpdateReportTypeCommandValidator _validator;

        public UpdateReportTypeTest(ReportTypeFakeData fakeData) : base(fakeData)
        {
            this._command = new UpdateReportTypeCommand();
            this._validator = new UpdateReportTypeCommandValidator();
            this._handler = new UpdateReportTypeCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfIdIfEmpty()
        {
            _command.ReportTypeDescription = "Offensive Language";
            _command.ReportTypeName = "Offensive Language";
            TestValidationResult<UpdateReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeNameIfEmpty()
        {
            _command.ReportTypeDescription = "Offensive Language";
            _command.Id = 1;

            TestValidationResult<UpdateReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportTypeName);
        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeDescriptionIfEmpty()
        {
            _command.ReportTypeName = "Offensive Language";
            _command.Id = 1;
            TestValidationResult<UpdateReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportTypeDescription);
        }
        [Fact]
        public async Task ReportTypeCannotBeDuplicatedWhenUpdated()
        {
            _command.Id = 1;
            _command.ReportTypeName = "Rahatsız Edici Söylem";
            _command.ReportTypeDescription = "Test";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });

        }
    }
}
