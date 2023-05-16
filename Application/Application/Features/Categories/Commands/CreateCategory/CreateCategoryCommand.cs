using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryIds { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly CategoryBusinessRules categoryBusinessRules;
            public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
            {
                this.categoryRepository = categoryRepository;
                this.categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                await categoryBusinessRules.CategoryCannotBeDuplicatedWhenInserted(request.CategoryName);
                
                Category category = new()
                {
                    CategoryName = request.CategoryName,
                    CategoryId=request?.ParentCategoryIds
                };
                

                Category createdCategory = await categoryRepository.AddAsync(category);

                return new CreateCategoryResponse { CategoryName = createdCategory.CategoryName, Id = createdCategory.Id,ParentCategroyId= createdCategory.CategoryId };
            }
        }
    }
}
