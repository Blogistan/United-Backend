using Application.Features.Categories.Commands.UpdateRangeCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.UpdateRangeCategory.UpdateRangeCategoryCommand;

namespace AuthTest.Features.Categories.Commands.UpdateRange
{
    public class UpdateRangeCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly UpdateRangeCategoryCommand updateRangeCategoryCommand;
        private readonly UpdateRangeCategoryCommandHandler updaterangeCategoryCommandHandler;
        private readonly UpdateRangeCategoryCommandValidator validationRules;
        public UpdateRangeCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            updateRangeCategoryCommand = new UpdateRangeCategoryCommand();
            validationRules = new UpdateRangeCategoryCommandValidator();
            updaterangeCategoryCommandHandler = new UpdateRangeCategoryCommandHandler(categoryRepository, Mapper, categoryBusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfDtoIsEmpty()
        {
            TestValidationResult<UpdateRangeCategoryCommand> testValidationResult = validationRules.TestValidate(updateRangeCategoryCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.UpdateCategoryDtos);
        }
        [Fact]
        public async Task CategoryCannotBeDuplicatedWhenUpdated()
        {
            updateRangeCategoryCommand.UpdateCategoryDtos = new List<Application.Features.Categories.Dtos.UpdateCategoryDto>
            {
                new Application.Features.Categories.Dtos.UpdateCategoryDto{Id=1,CategoryName="TestCat123"},
                new Application.Features.Categories.Dtos.UpdateCategoryDto{Id=2,CategoryName="TestCat2"},
            };

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await updaterangeCategoryCommandHandler.Handle(updateRangeCategoryCommand, CancellationToken.None);
            });
        }
    }
}
