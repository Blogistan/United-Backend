using Application.Features.Blogs.Queries.DecreaseSuprisedBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseSuprisedBlog.DecreaseSurprisedBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SupriseBlog
{
    public class DecreaseSuprisedBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseSurprisedBlogQuery _query;
        private readonly DecreaseSurprisedBlogQueryValidator _validator;
        private readonly DecreaseSurprisedBlogQueryHandler _handler;
        public DecreaseSuprisedBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseSurprisedBlogQuery();
            this._validator = new DecreaseSurprisedBlogQueryValidator();
            this._handler = new DecreaseSurprisedBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseSurprisedBlogQuery> testValidationResult = _validator.TestValidate(_query);

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
