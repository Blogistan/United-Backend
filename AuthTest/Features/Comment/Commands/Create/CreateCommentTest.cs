using Application.Features.Comments.Commands.CreateComment;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Commands.CreateComment.CreateCommentCommand;

namespace AuthTest.Features.Comment.Commands.Create
{
    public class CreateCommentTest : IClassFixture<Startup>
    {
        private readonly CreateCommentCommand createCommentCommand;
        private readonly CreateCommentCommandValidator validationRules;
        private readonly CreateCommentCommandHandler createCommentCommandHandler;
        private CommentFakeData commentFakeData;
        private SiteUserFakeData siteUserFakeData;


        public CreateCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;

            this.commentFakeData = commentFakeData;
            this.siteUserFakeData = siteUserFakeData;

            this.createCommentCommand = new CreateCommentCommand();
            this.validationRules = new CreateCommentCommandValidator();
            this.createCommentCommandHandler = new CreateCommentCommandHandler(commentRepository);
        }
        [Fact]
        public async Task ThrowExceptionIfCommentContentIsEmpty()
        {
            createCommentCommand.BlogId = 1;

            TestValidationResult<CreateCommentCommand> testValidationResult = validationRules.TestValidate(createCommentCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentContent);
        }
        [Fact]
        public async Task ThrowExceptionIfBlogIDIsEmpty()
        {
            createCommentCommand.CommentContent = "aasdasd";

            TestValidationResult<CreateCommentCommand> testValidationResult = validationRules.TestValidate(createCommentCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task CreateCommentSuccessFully()
        {
            createCommentCommand.BlogId = 1;
            createCommentCommand.CommentContent = "TEST";
            createCommentCommand.UserId = 1;

            var result = await createCommentCommandHandler.Handle(createCommentCommand, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
