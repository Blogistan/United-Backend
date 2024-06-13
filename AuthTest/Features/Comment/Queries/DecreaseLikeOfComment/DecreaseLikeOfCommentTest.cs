using Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery.DecreaseLikeOfCommentQuery;

namespace AuthTest.Features.Comment.Queries.DecreaseLikeOfComment
{
    public class DecreaseLikeOfCommentTest:IClassFixture<Startup>
    {
        private readonly DecreaseLikeOfCommentQuery _query;
        private readonly DecreaseLikeOfCommentQueryValidator _validationRules;
        private readonly DecreaseLikeOfCommentQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;

        public DecreaseLikeOfCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);
            this._commentFakeData = commentFakeData;
            this._siteUserFakeData = siteUserFakeData;

            this._query = new DecreaseLikeOfCommentQuery();
            this._handler = new DecreaseLikeOfCommentQueryHandler(commentRepository, commentBusinessRules);
            this._validationRules = new DecreaseLikeOfCommentQueryValidator();
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
            TestValidationResult<DecreaseLikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
    }
}
