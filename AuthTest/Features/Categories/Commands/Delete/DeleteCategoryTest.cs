using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.DeleteCategory.DeleteCategoryCommand;

namespace AuthTest.Features.Categories.Commands.Delete
{
    public class DeleteCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly DeleteCategoryCommand deleteCategoryCommand;
        private readonly DeleteCategoryCommandHandler deleteCategoryCommandHandler;
        private readonly DeleteCategoryValidator validationRules;
        public DeleteCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            deleteCategoryCommand = new DeleteCategoryCommand();
            deleteCategoryCommandHandler = new DeleteCategoryCommandHandler(MockRepository.Object, categoryBusinessRules);
            validationRules = new DeleteCategoryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfIdIsEmpty()
        {
            TestValidationResult<DeleteCategoryCommand> testValidationResult = validationRules.TestValidate(deleteCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfcCategoryNotExists()
        {
            deleteCategoryCommand.Id = 123;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await deleteCategoryCommandHandler.Handle(deleteCategoryCommand, CancellationToken.None);

            });
        }
    }
}
