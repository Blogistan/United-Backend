using Application.Features.Blogs.Commands.UpdateBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Commands.UpdateBlog.UpdateBlogCommand;

namespace AuthTest.Features.Blogs.Commands.Update
{
    public class UpdateBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly UpdateBlogCommand _command;
        private readonly UpdateBlogCommandValidator _validator;
        private readonly UpdateBlogCommandHandler _handler;
        public UpdateBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._command = new UpdateBlogCommand();
            this._validator = new UpdateBlogCommandValidator();
            this._handler = new UpdateBlogCommandHandler(MockRepository.Object, BusinessRules, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfBlogIdIsEmpty()
        {
            TestValidationResult<UpdateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.CategoryId = 3;
            _command.WriterId = 2;

            TestValidationResult<UpdateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Title);
        }
        [Fact]
        public async Task ThrowExceptionIfWriterIdIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.CategoryId = 3;
            _command.Title = "Test title";

            TestValidationResult<UpdateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.WriterId);
        }
        [Fact]
        public async Task ThrowExceptionIfCategoryIdIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.WriterId = 3;
            _command.Title = "Test title";

            TestValidationResult<UpdateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
        [Fact]
        public async Task ThrowExceptionIfBannerImageUrlIsEmpty()
        {
            _command.CategoryId = 2;
            _command.WriterId = 3;
            _command.Title = "Test title";

            TestValidationResult<UpdateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BannerImageUrl);
        }
    }
}
