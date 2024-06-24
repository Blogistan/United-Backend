using Application.Features.ReportTypes.Commands.CreateReportType;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.ReportTypes.Commands.CreateReportType.CreateReportTypeCommand;

namespace AuthTest.Features.ReportTypes.Command.Create
{
    public class CreateReportTypeTest : ReportTypeMockRepository, IClassFixture<Startup>
    {

        private readonly CreateReportTypeCommand _command;
        private readonly CreateReportTypeCommandHandler _handler;
        private readonly CreateReportTypeCommandValidator _validator;

        public CreateReportTypeTest(ReportTypeFakeData fakeData) : base(fakeData)
        {
            this._command = new CreateReportTypeCommand();
            this._validator = new CreateReportTypeCommandValidator();
            this._handler = new CreateReportTypeCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeNameIfEmpty()
        {
            _command.ReportTypeDescription = "Offensive Language";

            TestValidationResult<CreateReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportTypeName);
        }
        [Fact]
        public async Task ThrowExceptionIfReportTypeDescriptionIfEmpty()
        {
            _command.ReportTypeName = "Offensive Language";

            TestValidationResult<CreateReportTypeCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.ReportTypeDescription);
        }
        [Fact]
        public async Task ReportTypeCannotBeDuplicatedWhenInserted()
        {
            _command.ReportTypeName = "Taciz";
            _command.ReportTypeDescription = "Test";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });

        }
    }
}
