using Application.Features.Blogs.Queries.GetListBlogDynamic;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using static Application.Features.Blogs.Queries.GetListBlogDynamic.GetListBlogDynamicQuery;

namespace AuthTest.Features.Blogs.Queries.GetListDynamic
{
    public class GetListBlogDynamicTest: BlogMockRepository, IClassFixture<Startup>
    {
        private readonly GetListBlogDynamicQuery _query;
        private readonly GetListBlogDynamicQueryHandler _handler;
        public GetListBlogDynamicTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new GetListBlogDynamicQuery();
            this._handler = new GetListBlogDynamicQueryHandler(MockRepository.Object, Mapper);
        }
        //[Fact]
        //public async Task ThrowExceptionIfPageRequestIsEmpty()
        //{
        //    TestValidationResult<GetListBlogDynamicQuery> testValidationResult = _validator.TestValidate(_query);
        //    testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        //}
    }
}
