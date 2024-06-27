using Application.Features.Blogs.Queries.SadBlog;
using Application.Features.Blogs.Queries.SuprisedBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.SuprisedBlog.SuprisedBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SupriseBlog
{
    public class SuprisedBlogQueryTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly SuprisedBlogQuery _query;
        private readonly SuprisedBlogQueryValidator _validator;
        private readonly SuprisedBlogQueryHandler _handler;
        public SuprisedBlogQueryTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new SuprisedBlogQuery();
            this._validator = new SuprisedBlogQueryValidator();
            this._handler = new SuprisedBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<SuprisedBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSuprisedCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionSuprisedCount);
        }
    }
}
