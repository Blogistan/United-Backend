using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.CreateCategory.CreateCategoryCommand;

namespace AuthTest.Features.Categories.Commands.Create
{
    public class CreateCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly CreateCategoryCommand createCategoryCommand;
        private readonly CreateCategoryCommandHandler createCategoryCommandHandler;
        private readonly CreateCategoryValidator validationRules;
        public CreateCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            createCategoryCommand = new CreateCategoryCommand();
            validationRules = new CreateCategoryValidator();
            createCategoryCommandHandler = new CreateCategoryCommandHandler(categoryRepository, categoryBusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfCategoryNameEmpty()
        {
            TestValidationResult<CreateCategoryCommand> testValidationResult = validationRules.TestValidate(createCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CategoryName);
        }
        [Fact]
        public async Task ThowExceptionIfCategoryAlreadyExists()
        {
            createCategoryCommand.CategoryName = "TestCat1";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);
            });
        }

    }
}
