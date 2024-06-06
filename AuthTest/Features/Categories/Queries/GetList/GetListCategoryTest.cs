using Application.Features.Categories.Queries.GetListCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Queries.GetListCategory.GetListCategoryQuery;

namespace AuthTest.Features.Categories.Queries.GetList
{
    public class GetListCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly GetListCategoryQuery getListCategoryQuery;
        private readonly GetListCategoryQueryHandler getListCategoryQueryHandler;
        private readonly GetListCategoryQueryValidator validationRules;
        public GetListCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            getListCategoryQuery = new GetListCategoryQuery();
            validationRules = new GetListCategoryQueryValidator();
            getListCategoryQueryHandler = new GetListCategoryQueryHandler(categoryRepository, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            TestValidationResult<GetListCategoryQuery> testValidationResult = validationRules.TestValidate(getListCategoryQuery);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task GetDataSuccessfully()
        {
            getListCategoryQuery.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };

            var result = await getListCategoryQueryHandler.Handle(getListCategoryQuery, CancellationToken.None);

            Assert.NotEmpty(result.Items);
        }
    }
}
