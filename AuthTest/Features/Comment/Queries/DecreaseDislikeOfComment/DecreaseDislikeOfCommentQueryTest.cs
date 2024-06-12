using Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery.DecreaseDislikeOfCommentQuery;

namespace AuthTest.Features.Comment.Queries.DecreaseDislikeOfComment
{
    public class DecreaseDislikeOfCommentQueryTest : IClassFixture<Startup>
    {
        private readonly DecreaseDislikeOfCommentQuery _query;
        private readonly DecreaseDislikeOfCommentQueryValidator _validationRules;
        private readonly DecreaseDislikeOfCommentQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;


        public DecreaseDislikeOfCommentQueryTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);
            this._commentFakeData = commentFakeData;
            this._siteUserFakeData = siteUserFakeData;

            this._query = new DecreaseDislikeOfCommentQuery();
            this._handler = new DecreaseDislikeOfCommentQueryHandler(commentRepository, commentBusinessRules);
            this._validationRules = new DecreaseDislikeOfCommentQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfCommentNotExists()
        {
            _query.CommentId = 15341;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_query, CancellationToken.None);
            });
        }
        [Fact]
        public async Task DecreaseDislikeSuccessfully()
        {
            _query.CommentId = 1;
            var result = await _handler.Handle(_query, CancellationToken.None);
            Assert.NotNull(result);

        }
        [Fact]
        public async Task ThrowExceptinIfCommentIdIsEmpty()
        {
            TestValidationResult<DecreaseDislikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }

    }
}
