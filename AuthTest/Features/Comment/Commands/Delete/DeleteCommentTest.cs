using Application.Features.Comments.Commands.DeleteComment;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Commands.DeleteComment.DeleteCommentCommand;

namespace AuthTest.Features.Comment.Commands.Delete
{
    public class DeleteCommentTest : IClassFixture<Startup>
    {
        private readonly DeleteCommentCommand deleteCommentCommand;
        private readonly DeleteCommentCommandValidator validationRules;
        private readonly DeleteCommentCommandHandler deleteCommentCommandHandler;
        private CommentFakeData commentFakeData;
        private SiteUserFakeData siteUserFakeData;


        public DeleteCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);
            this.commentFakeData = commentFakeData;
            this.siteUserFakeData = siteUserFakeData;

            this.deleteCommentCommand = new DeleteCommentCommand();
            this.validationRules = new DeleteCommentCommandValidator();
            this.deleteCommentCommandHandler = new DeleteCommentCommandHandler(commentRepository, commentBusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfCommentNotExists()
        {
            deleteCommentCommand.Id = 15341;
            deleteCommentCommand.Permanent = false;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await deleteCommentCommandHandler.Handle(deleteCommentCommand, CancellationToken.None);
            });

        }
        [Fact]
        public async Task ThrowExceptionIfPermanentIsNull()
        {
            TestValidationResult<DeleteCommentCommand> testValidationResult = validationRules.TestValidate(deleteCommentCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Permanent);
        }

    }
}
