using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategroyId { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly CategoryBusinessRules categoryBusinessRules;
            public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
            {
                this.categoryRepository = categoryRepository;
                this.categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                await categoryBusinessRules.CategoryCannotBeDuplicatedWhenUpdated(request.CategoryName);

                Category category = new()
                {
                    Id = request.Id,
                    CategoryName = request.CategoryName,
                    CategoryId = request.ParentCategroyId
                };

                Category updatedCategory = await categoryRepository.UpdateAsync(category);

                return new UpdateCategoryResponse
                {
                    Id = updatedCategory.Id,
                    CategoryName = updatedCategory.CategoryName,
                    ParentCategroyId = (int)updatedCategory.CategoryId
                };
            }
        }

    }
}
