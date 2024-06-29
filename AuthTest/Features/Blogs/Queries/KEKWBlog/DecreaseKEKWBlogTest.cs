using Application.Features.Blogs.Queries.DecreaseKEKWBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseKEKWBlog.DecreaseKEKWBlogQuery;

namespace AuthTest.Features.Blogs.Queries.KEKWBlog
{
    public class DecreaseKEKWBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseKEKWBlogQuery _query;
        private readonly DecreaseKEKWBlogQueryValidator _validator;
        private readonly DecreaseKEKWBlogQueryHandler _handler;
        public DecreaseKEKWBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseKEKWBlogQuery();
            this._validator = new DecreaseKEKWBlogQueryValidator();
            this._handler = new DecreaseKEKWBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseKEKWBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseKekwCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(-1, result.ReactionKEKWCount);
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
