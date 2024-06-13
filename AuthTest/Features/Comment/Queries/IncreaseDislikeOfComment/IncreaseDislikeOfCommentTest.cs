using Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery.IncreaseDislikeOfCommentQuery;

namespace AuthTest.Features.Comment.Queries.IncreaseDislikeOfComment
{
    public class IncreaseDislikeOfCommentTest:IClassFixture<Startup>
    {
        private readonly IncreaseDislikeOfCommentQuery _query;
        private readonly IncreaseDislikeOfCommentQueryValidator _validationRules;
        private readonly IncreaseDislikeOfCommentQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;


        public IncreaseDislikeOfCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {
            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            this._commentFakeData = commentFakeData;
            this._siteUserFakeData = siteUserFakeData;

            this._query = new IncreaseDislikeOfCommentQuery();
            this._handler = new IncreaseDislikeOfCommentQueryHandler(commentRepository);
            this._validationRules = new IncreaseDislikeOfCommentQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfCommentIdIsNull()
        {
            TestValidationResult<IncreaseDislikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
    }
}
