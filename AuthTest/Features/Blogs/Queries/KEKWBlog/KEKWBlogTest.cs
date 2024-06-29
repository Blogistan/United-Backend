using Application.Features.Blogs.Queries.KEKWBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.KEKWBlog.KekwBlogQuery;

namespace AuthTest.Features.Blogs.Queries.KEKWBlog
{
    public class KEKWBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly KekwBlogQuery _query;
        private readonly KekwBlogQueryValidator _validator;
        private readonly KekwBlogQueryHandler _handler;
        public KEKWBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new KekwBlogQuery();
            this._validator = new KekwBlogQueryValidator();
            this._handler = new KekwBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<KekwBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseKekwCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionKEKWCount);
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
