using Application.Features.Blogs.Commands.CreateBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Commands.CreateBlog.CreateBlogCommand;

namespace AuthTest.Features.Blogs.Commands.Create
{
    public class CreateBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly CreateBlogCommand _command;
        private readonly CreateBlogCommandValidator _validator;
        private readonly CreateBlogCommandHandler _handler;
        public CreateBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._command = new CreateBlogCommand();
            this._validator = new CreateBlogCommandValidator();
            this._handler = new CreateBlogCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.CategoryId = 3;
            _command.WriterId = 2;

            TestValidationResult<CreateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Title);
        }
        [Fact]
        public async Task ThrowExceptionIfWriterIdIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.CategoryId = 3;
            _command.Title = "Test title";

            TestValidationResult<CreateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.WriterId);
        }
        [Fact]
        public async Task ThrowExceptionIfCategoryIdIsEmpty()
        {
            _command.BannerImageUrl = "test2132";
            _command.WriterId = 3;
            _command.Title = "Test title";

            TestValidationResult<CreateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }
        [Fact]
        public async Task ThrowExceptionIfBannerImageUrlIsEmpty()
        {
            _command.CategoryId = 2;
            _command.WriterId = 3;
            _command.Title = "Test title";

            TestValidationResult<CreateBlogCommand> testValidationResult = _validator.TestValidate(_command);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BannerImageUrl);
        }
        [Fact]
        public async Task BlogCannotBeDuplicatedWhenInserted()
        {
            _command.CategoryId = 1;
            _command.WriterId = 1;
            _command.BannerImageUrl = "Img1";
            _command.Title = "Test Blog";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_command, CancellationToken.None);
            });
        }
    }
}
