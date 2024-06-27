using Application.Features.Blogs.Commands.DeleteBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Commands.DeleteBlog.DeleteBlogCommand;

namespace AuthTest.Features.Blogs.Commands.Delete
{
    public class DeleteBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DeleteBlogCommand _command;
        private readonly DeleteBlogCommandValidator _validator;
        private readonly DeleteBlogCommandHandler _handler;
        public DeleteBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._command = new DeleteBlogCommand();
            this._validator = new DeleteBlogCommandValidator();
            this._handler = new DeleteBlogCommandHandler(MockRepository.Object, BusinessRules, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfIdIsEmpty()
        {
            TestValidationResult<DeleteBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfBlogNotExists()
        {
            _command.Id = 54165;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
    }
}
