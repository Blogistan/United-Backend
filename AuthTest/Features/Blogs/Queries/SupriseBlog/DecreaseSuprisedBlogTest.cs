using Application.Features.Blogs.Queries.DecreaseSuprisedBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseSuprisedBlog.DecreaseSuprisedBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SupriseBlog
{
    public class DecreaseSuprisedBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseSuprisedBlogQuery _query;
        private readonly DecreaseSuprisedBlogQueryValidator _validator;
        private readonly DecreaseSuprisedBlogQueryHandler _handler;
        public DecreaseSuprisedBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseSuprisedBlogQuery();
            this._validator = new DecreaseSuprisedBlogQueryValidator();
            this._handler = new DecreaseSuprisedBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseSuprisedBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSuprisedCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(-1, result.ReactionSuprisedCount);
        }
        [Fact]
        public async Task ThrowExceptionIfBlogNotExists()
        {
            _query.BlogId = 54165;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_query, CancellationToken.None);
            });
        }
    }
}
