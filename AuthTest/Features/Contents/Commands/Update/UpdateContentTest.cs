using Application.Features.Contents.Commands.UpdateContent;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Contents.Commands.UpdateContent.UpdateContentCommand;

namespace AuthTest.Features.Contents.Commands.Update
{
    public class UpdateContentTest:IClassFixture<Startup>
    {
        private readonly ContentFakeData contentFakeData;
        private readonly UpdateContentCommand _command;
        private readonly UpdateContentCommandValidator _validator;
        private readonly UpdateContentCommandHandler _handler;

        public UpdateContentTest(ContentFakeData contentFakeData)
        {
            this.contentFakeData = contentFakeData;

            IContentRepository contentRepository = ContentMockRepository.GetContentMockRepository(contentFakeData).Object;

            ContentBusinessRules contentBusinessRules = new ContentBusinessRules(contentRepository);

            this._handler = new UpdateContentCommandHandler(contentRepository, contentBusinessRules);
            this._validator = new UpdateContentCommandValidator();
            this._command = new UpdateContentCommand();
        }
        [Fact]
        public async Task ContentIdShouldNotEmpty()
        {
            _command.Title = "A";
            _command.ContentPragraph = "TEST";
            TestValidationResult<UpdateContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ContentTitleShouldNotEmpty()
        {
            _command.Id=1;
            _command.ContentPragraph = "TEST";
            TestValidationResult<UpdateContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Title);
        }
        [Fact]
        public async Task ContentContentPragraphShouldNotEmpty()
        {
            _command.Id = 1;
            _command.Title = "TEST";
            TestValidationResult<UpdateContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.ContentPragraph);
        }
        [Fact]
        public async Task ThrowExceptionIfContentNotExists()
        {
            _command.Id = 5153;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            _command.Id = 1;
            _command.ContentPragraph = "TEST";
            _command.Title = "TEST";
            var result = await _handler.Handle(_command, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
