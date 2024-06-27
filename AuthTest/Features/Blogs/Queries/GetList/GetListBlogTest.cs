using Application.Features.Blogs.Queries.GetListBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.GetListBlog.GetListBlogQuery;

namespace AuthTest.Features.Blogs.Queries.GetList
{
    public class GetListBlogTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly GetListBlogQuery _query;
        private readonly GetListBlogQueryValidator _validator;
        private readonly GetListBlogQueryHandler _handler;
        public GetListBlogTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new GetListBlogQuery();
            this._validator = new GetListBlogQueryValidator();
            this._handler = new GetListBlogQueryHandler(MockRepository.Object, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            TestValidationResult<GetListBlogQuery> testValidationResult = _validator.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            _query.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };
            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
