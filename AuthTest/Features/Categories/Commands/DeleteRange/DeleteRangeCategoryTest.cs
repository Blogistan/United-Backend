using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.DeleteRangeCategory;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Categories.Commands.DeleteCategory.DeleteCategoryCommand;
using static Application.Features.Categories.Commands.DeleteRangeCategory.DeleteRangeCategoryCommand;

namespace AuthTest.Features.Categories.Commands.DeleteRange
{
    public class DeleteRangeCategoryTest : CategoryMockRepository, IClassFixture<Startup>
    {
        private readonly CategoryFakeData categoryFakeData;
        private readonly DeleteRangeCategoryCommand deleteRangeCategoryCommand;
        private readonly DeleteRangeCategoryCommandHandler deleteRangeCategoryCommandHandler;
        private readonly DeleteRangeCategoryCommandValidator validationRules;
        public DeleteRangeCategoryTest(CategoryFakeData fakeData) : base(fakeData)
        {

            ICategoryRepository categoryRepository = MockRepository.Object;
            CategoryBusinessRules categoryBusinessRules = BusinessRules;

            categoryFakeData = fakeData;
            deleteRangeCategoryCommand = new DeleteRangeCategoryCommand();
            validationRules = new DeleteRangeCategoryCommandValidator();
            deleteRangeCategoryCommandHandler = new DeleteRangeCategoryCommandHandler(categoryRepository, Mapper, categoryBusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfDtoIsEmpty()
        {
            TestValidationResult<DeleteRangeCategoryCommand> testValidationResult = validationRules.TestValidate(deleteRangeCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.DeleteRangeCategoryDtos);
        }
        [Fact]
        public async Task ThrowExceptionIfPermanentNotExists()
        {
            deleteRangeCategoryCommand.DeleteRangeCategoryDtos = new List<Application.Features.Categories.Dtos.DeleteRangeCategoryDto>
            {
                new Application.Features.Categories.Dtos.DeleteRangeCategoryDto{Id=1}
            };
            TestValidationResult<DeleteRangeCategoryCommand> testValidationResult = validationRules.TestValidate(deleteRangeCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Permanent);
        }
        [Fact]
        public async Task ThrowExceptionIfcCategoryNotExists()
        {
            deleteRangeCategoryCommand.DeleteRangeCategoryDtos = new List<Application.Features.Categories.Dtos.DeleteRangeCategoryDto>
            {
                new Application.Features.Categories.Dtos.DeleteRangeCategoryDto{Id=1123}
            };
            TestValidationResult<DeleteRangeCategoryCommand> testValidationResult = validationRules.TestValidate(deleteRangeCategoryCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.Permanent);
        }
    }
}
