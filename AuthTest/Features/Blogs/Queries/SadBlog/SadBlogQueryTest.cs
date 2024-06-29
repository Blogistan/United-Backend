using Application.Features.Blogs.Queries.SadBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.SadBlog.SadBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SadBlog
{
    public class SadBlogQueryTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly SadBlogQuery _query;
        private readonly SadBlogQueryValidator _validator;
        private readonly SadBlogQueryHandler _handler;
        public SadBlogQueryTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new SadBlogQuery();
            this._validator = new SadBlogQueryValidator();
            this._handler = new SadBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<SadBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSadCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionSadCount);
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
