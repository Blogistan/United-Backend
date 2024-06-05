using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.UpdateCategory.UpdateCategoryCommand;

namespace AuthTest.Features.Categories.Update
{
    public class UpdateCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly UpdateCategoryCommand updateCategoryCommand;
        private readonly UpdateCategoryCommandHandler updateCategoryCommandHandler;
        private readonly UpdateCategoryValidator validationRules;
        public UpdateCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            this.categoryFakeData = fakeData;
            this.updateCategoryCommand = new UpdateCategoryCommand();
            this.updateCategoryCommandHandler = new UpdateCategoryCommandHandler(MockRepository.Object, categoryBusinessRules);
            this.validationRules = new UpdateCategoryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfIdIsEmpty()
        {
            updateCategoryCommand.CategoryName = "asd";

            TestValidationResult<UpdateCategoryCommand> testValidationResult = validationRules.TestValidate(updateCategoryCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task ThrowExceptionIfNameIsEmpty()
        {
            updateCategoryCommand.Id = 1;

            TestValidationResult<UpdateCategoryCommand> testValidationResult = validationRules.TestValidate(updateCategoryCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.CategoryName);
        }
        [Fact]
        public async Task CategoryCannotBeDuplicatedWhenUpdated()
        {
            updateCategoryCommand.CategoryName = "TestCat1";
            updateCategoryCommand.Id = 1;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await updateCategoryCommandHandler.Handle(updateCategoryCommand, CancellationToken.None);
            });
        }
    }
}
