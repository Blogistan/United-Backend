using Application.Features.Comments.Commands.UpdateComment;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Commands.UpdateComment.UpdateCommentCommand;

namespace AuthTest.Features.Comment.Commands.Update
{
    public class UpdateCommentTest : IClassFixture<Startup>
    {
        private readonly UpdateCommentCommand updateCommentCommand;
        private readonly UpdateCommentCommandValidator validationRules;
        private readonly UpdateCommentCommandHandler updateCommentCommandHandler;
        private CommentFakeData commentFakeData;
        private SiteUserFakeData siteUserFakeData;


        public UpdateCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);
            this.commentFakeData = commentFakeData;
            this.siteUserFakeData = siteUserFakeData;

            this.updateCommentCommand = new UpdateCommentCommand();
            this.validationRules = new UpdateCommentCommandValidator();
            this.updateCommentCommandHandler = new UpdateCommentCommandHandler(commentRepository, commentBusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfCommentNotExists()
        {
            updateCommentCommand.Id = 15341;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await updateCommentCommandHandler.Handle(updateCommentCommand, CancellationToken.None);
            });

        }
        [Fact]
        public async Task ThrowExceptionIfCommentContentIsNull()
        {
            updateCommentCommand.Id = 15341;
            TestValidationResult<UpdateCommentCommand> testValidationResult = validationRules.TestValidate(updateCommentCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentContent);
        }
    }
}
