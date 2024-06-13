using Application.Features.Comments.Profiles;
using Application.Features.Comments.Queries.GetBlogCommentsQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.GetBlogCommentsQuery.GetBlogCommentsQuery;

namespace AuthTest.Features.Comment.Queries.GetBlogComments
{
    public class GetBlogCommentsQueryTest : IClassFixture<Startup>
    {
        private readonly GetBlogCommentsQuery _query;
        private readonly GetBlogCommentsQueryValidator _validationRules;
        private readonly GetBlogCommentsQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;

        public GetBlogCommentsQueryTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
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

            this._query = new GetBlogCommentsQuery();
            this._handler = new GetBlogCommentsQueryHandler(commentRepository, mapper, commentBusinessRules);
            this._validationRules = new GetBlogCommentsQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfBlogIdIsNull()
        {
            TestValidationResult<GetBlogCommentsQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task GetBlogCommentsSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result.commentViewDto);
        }
    }
}
