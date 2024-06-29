using Application.Features.Blogs.Queries.BlogDetailById;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.BlogDetailById.BlogDetailByIdQuery;

namespace AuthTest.Features.Blogs.Queries.BlogDetailById
{
    public class BlogDetailByIdTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly BlogDetailByIdQuery _query;
        private readonly BlogDetailByIdQueryValidator _validator;
        private readonly BlogDetailByIdQueryHandler _handler;
        public BlogDetailByIdTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new BlogDetailByIdQuery();
            this._validator = new BlogDetailByIdQueryValidator();
            this._handler = new BlogDetailByIdQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<BlogDetailByIdQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
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
