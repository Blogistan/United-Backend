using Application.Features.Blogs.Queries.DecreaseSadBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseSadBlog.DecreaseSadBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SadBlog
{
    public class DecreaseSadBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseSadBlogQuery _query;
        private readonly DecreaseSadBlogQueryValidator _validator;
        private readonly DecreaseSadBlogQueryHandler _handler;
        public DecreaseSadBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseSadBlogQuery();
            this._validator = new DecreaseSadBlogQueryValidator();
            this._handler = new DecreaseSadBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseSadBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSadCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(-1, result.ReactionSadCount);
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
