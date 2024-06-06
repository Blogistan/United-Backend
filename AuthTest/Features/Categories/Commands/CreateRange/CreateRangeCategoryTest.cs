using Application.Features.Categories.Commands.CreateRangeCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.CreateRangeCategory.CreateRangeCategoryCommand;

namespace AuthTest.Features.Categories.Commands.CreateRange
{
    public class CreateRangeCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly CreateRangeCategoryCommand createRangeCategoryCommand;
        private readonly CreateRangeCategoryCommandHandler createrangeCategoryCommandHandler;
        private readonly CreateRangeCategoryCommandValidator validationRules;
        public CreateRangeCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            createRangeCategoryCommand = new CreateRangeCategoryCommand();
            validationRules = new CreateRangeCategoryCommandValidator();
            createrangeCategoryCommandHandler = new CreateRangeCategoryCommandHandler(categoryRepository, categoryBusinessRules, Mapper);
        }
        [Fact]
        public async Task ThrowExcepitonIfDtoIsEmpty()
        {
            TestValidationResult<CreateRangeCategoryCommand> testValidationResult = validationRules.TestValidate(createRangeCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CreateCategoryDtos);
        }
        [Fact]
        public async Task ThowExceptionIfCategoryAlreadyExists()
        {
            createRangeCategoryCommand.CreateCategoryDtos = new List<Application.Features.Categories.Dtos.CreateCategoryDto>()
            {
                new Application.Features.Categories.Dtos.CreateCategoryDto{CategoryName="Test1"},
                new Application.Features.Categories.Dtos.CreateCategoryDto{CategoryName="TestCat1"},
            };

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await createrangeCategoryCommandHandler.Handle(createRangeCategoryCommand, CancellationToken.None);
            });
        }
    }
}
