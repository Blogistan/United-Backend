using Application.Features.Blogs.Queries.SuprisedBlog;
using Application.Features.Blogs.Queries.TriggerBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.TriggerBlog.TriggerBlogQuery;

namespace AuthTest.Features.Blogs.Queries.TriggerBlog
{
    public class TriggerBlogQueryTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly TriggerBlogQuery _query;
        private readonly TriggerBlogQueryValidator _validator;
        private readonly TriggerBlogQueryHandler _handler;
        public TriggerBlogQueryTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new TriggerBlogQuery();
            this._validator = new TriggerBlogQueryValidator();
            this._handler = new TriggerBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<TriggerBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseTriggerCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionSuprisedCount);
        }
    }
}
