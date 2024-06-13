using Application.Features.Comments.Profiles;
using Application.Features.Comments.Queries.GetBlogCommentsQuery;
using Application.Features.Comments.Queries.GetCommentResponsesQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.GetCommentResponsesQuery.GetCommentResponsesQuery;

namespace AuthTest.Features.Comment.Queries.GetCommentResponses
{
    public class GetCommentResponsesTest : IClassFixture<Startup>
    {
        private readonly GetCommentResponsesQuery _query;
        private readonly GetCommentResponsesQueryValidator _validationRules;
        private readonly GetCommentResponsesQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;

        public GetCommentResponsesTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
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

            this._query = new GetCommentResponsesQuery();
            this._handler = new GetCommentResponsesQueryHandler(commentRepository, mapper, commentBusinessRules);
            this._validationRules = new GetCommentResponsesQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfCommentIsNull()
        {
            TestValidationResult<GetCommentResponsesQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
        [Fact]
        public async Task GetCommentRespones()
        {
            _query.CommentId = 3;
            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result.commentViewDto);
        }
    }
}
