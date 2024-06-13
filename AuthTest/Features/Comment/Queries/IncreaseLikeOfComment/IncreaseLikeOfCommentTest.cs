using Application.Features.Comments.Profiles;
using Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery.IncreaseLikeOfCommentQuery;

namespace AuthTest.Features.Comment.Queries.IncreaseLikeOfComment
{
    public class IncreaseLikeOfCommentTest:IClassFixture<Startup>
    {
        private readonly IncreaseLikeOfCommentQuery _query;
        private readonly IncreaseLikeOfCommentQueryValidator _validationRules;
        private readonly IncreaseLikeOfCommentQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;

        public IncreaseLikeOfCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);

            MapperConfiguration mapperConfig = new(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            this._commentFakeData = commentFakeData;
            this._siteUserFakeData = siteUserFakeData;

            this._query = new IncreaseLikeOfCommentQuery();
            this._handler = new IncreaseLikeOfCommentQueryHandler(commentRepository);
            this._validationRules = new IncreaseLikeOfCommentQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfCommentIsNull()
        {
            TestValidationResult<IncreaseLikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
        [Fact]
        public async Task IncreaseLikeSuccessfully()
        {
            _query.CommentId = 1;
            var result = await _handler.Handle(_query, CancellationToken.None);
            Assert.Equal(1, result.Likes);

        }
        [Fact]
        public async Task ThrowExceptinIfCommentIdIsEmpty()
        {
            TestValidationResult<IncreaseLikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
    }
}
