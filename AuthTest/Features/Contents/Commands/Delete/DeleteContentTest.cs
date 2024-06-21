using Application.Features.Contents.Commands.DeleteContent;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Contents.Commands.DeleteContent.DeleteContentCommand;

namespace AuthTest.Features.Contents.Commands.Delete
{
    public class DeleteContentTest : IClassFixture<Startup>
    {
        private readonly ContentFakeData contentFakeData;
        private readonly DeleteContentCommand _command;
        private readonly DeleteContentCommandValidator _validator;
        private readonly DeleteContentCommandHandler _handler;

        public DeleteContentTest(ContentFakeData contentFakeData)
        {
            this.contentFakeData = contentFakeData;

            IContentRepository contentRepository = ContentMockRepository.GetContentMockRepository(contentFakeData).Object;

            ContentBusinessRules contentBusinessRules = new ContentBusinessRules(contentRepository);

            this._handler = new DeleteContentCommandHandler(contentRepository, contentBusinessRules);
            this._validator = new DeleteContentCommandValidator();
            this._command = new DeleteContentCommand();
        }
        [Fact]
        public async Task ContentIdShouldNotEmpty()
        {
            TestValidationResult<DeleteContentCommand> testValidationResult = _validator.TestValidate(_command);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
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
        public async Task ShouldDeleteSuccessfully()
        {
            _command.Id = 1;

            var result = await _handler.Handle(_command, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
