using Application.Features.Categories.Queries.GetListCategoryByDynamic;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Queries.GetListCategoryByDynamic.GetListCategoryQueryByDynamicQuery;

namespace AuthTest.Features.Categories.Queries.GetListDynamic
{
    public class GetListCategoryByDynamicTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly GetListCategoryQueryByDynamicQuery getListCategoryQueryByDynamicQuery;
        private readonly GetListCategoryQueryByDynamicQueryHandler getListCategoryQueryByDynamicQueryHandler;
        private readonly GetListCategoryDynamicValidator validationRules;
        public GetListCategoryByDynamicTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            getListCategoryQueryByDynamicQuery = new GetListCategoryQueryByDynamicQuery();
            validationRules = new GetListCategoryDynamicValidator();
            getListCategoryQueryByDynamicQueryHandler = new GetListCategoryQueryByDynamicQueryHandler(categoryRepository, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            TestValidationResult<GetListCategoryQueryByDynamicQuery> testValidationResult = validationRules.TestValidate(getListCategoryQueryByDynamicQuery);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task ThrowExceptionIfDynamicIsEmpty()
        {
            getListCategoryQueryByDynamicQuery.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };
            TestValidationResult<GetListCategoryQueryByDynamicQuery> testValidationResult = validationRules.TestValidate(getListCategoryQueryByDynamicQuery);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Dynamic);
        }
    }
}
