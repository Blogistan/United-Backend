using Application.Features.Contents.Commands.CreateContent;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Contents.Commands.CreateContent.CreateContentCommand;

namespace AuthTest.Features.Contents.Commands.Create
{
    public class CreateContentTest : IClassFixture<Startup>
    {
        private readonly ContentFakeData contentFakeData;
        private readonly CreateContentCommand _command;
        private readonly CreateContentCommandValidator _validator;
        private readonly CreateContentCommandHandler _handler;

        public CreateContentTest(ContentFakeData contentFakeData)
        {
            this.contentFakeData = contentFakeData;

            IContentRepository contentRepository = ContentMockRepository.GetContentMockRepository(contentFakeData).Object;

            this._handler = new CreateContentCommandHandler(contentRepository);
            this._validator = new CreateContentCommandValidator();
            this._command = new CreateContentCommand();
        }
        [Fact]
        public async Task ContentTitleShouldNotEmpty()
        {

            _command.ContentPragraph = "Test";
            TestValidationResult<CreateContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Title);
        }
        [Fact]
        public async Task ContentContentPragraphShouldNotEmpty()
        {

            _command.ContentPragraph="Test";
            TestValidationResult<CreateContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Title);
        }

    }
}
