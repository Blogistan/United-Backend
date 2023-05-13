using Application.Features.Categories.Dtos;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.UpdateRangeCategory
{
    public class UpdateRangeCategoryCommand : IRequest<UpdateRangeCategoryResponse>
    {
        public List<UpdateCategoryDto> UpdateCategoryDtos { get; set; }

        public class UpdateRangeCategoryCommandHandler : IRequestHandler<UpdateRangeCategoryCommand, UpdateRangeCategoryResponse>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly IMapper mapper;
            private readonly CategoryBusinessRules categoryBusinessRules;
            public UpdateRangeCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                this.categoryRepository = categoryRepository;
                this.mapper = mapper;
                this.categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<UpdateRangeCategoryResponse> Handle(UpdateRangeCategoryCommand request, CancellationToken cancellationToken)
            {
                await categoryBusinessRules.CategoryCannotBeDuplicatedWhenUpdated(request.UpdateCategoryDtos);

                List<Category> categories = mapper.Map<List<Category>>(request.UpdateCategoryDtos);


                var updatedCategories = await categoryRepository.UpdateRangeAsync(categories);

                List<CategoryViewDto> categoryViewDtos = mapper.Map<List<CategoryViewDto>>(updatedCategories);

                return new UpdateRangeCategoryResponse { CategoryViewDtos = categoryViewDtos };


            }
        }
    }
}
