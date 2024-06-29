using Application.Features.Blogs.Queries.DecreaseLovelyBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseLovelyBlog.DecreaseLovelyBLogQuery;

namespace AuthTest.Features.Blogs.Queries.LovelyBlog
{
    public class DecreaseLovelyBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseLovelyBLogQuery _query;
        private readonly DecreaseLovelyBLogQueryValidator _validator;
        private readonly DecreaseLovelyBLogQueryHandler _handler;
        public DecreaseLovelyBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseLovelyBLogQuery();
            this._validator = new DecreaseLovelyBLogQueryValidator();
            this._handler = new DecreaseLovelyBLogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseLovelyBLogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseLovelyCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(-1, result.ReactionLovelyCount);
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
