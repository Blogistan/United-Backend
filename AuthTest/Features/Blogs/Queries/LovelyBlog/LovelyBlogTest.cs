using Application.Features.Blogs.Queries.LovelyBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.LovelyBlog.LovelyBlogQuery;

namespace AuthTest.Features.Blogs.Queries.LovelyBlog
{
    public class LovelyBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly LovelyBlogQuery _query;
        private readonly LovelyBlogQueryValidator _validator;
        private readonly LovelyBlogQueryHandler _handler;
        public LovelyBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new LovelyBlogQuery();
            this._validator = new LovelyBlogQueryValidator();
            this._handler = new LovelyBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<LovelyBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseLovelyCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionLovelyCount);
        }
    }
}
