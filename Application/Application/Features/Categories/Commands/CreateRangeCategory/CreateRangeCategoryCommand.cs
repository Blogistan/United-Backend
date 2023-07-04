using Application.Features.Categories.Dtos;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public class CreateRangeCategoryCommand : IRequest<CreateRangeCategoryResponse>,ISecuredRequest
    {
        public List<CreateCategoryDto> CreateCategoryDtos { get; set; }
        public string[] Roles => new string[] { "Admin", "Moderator"};

        public class CreateRangeCategoryCommandHandler : IRequestHandler<CreateRangeCategoryCommand, CreateRangeCategoryResponse>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly CategoryBusinessRules categoryBusinessRules;
            private readonly IMapper mapper;
            public CreateRangeCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules, IMapper mapper)
            {
                this.categoryRepository = categoryRepository;
                this.categoryBusinessRules = categoryBusinessRules;
                this.mapper = mapper;
            }

            public async Task<CreateRangeCategoryResponse> Handle(CreateRangeCategoryCommand request, CancellationToken cancellationToken)
            {
                await categoryBusinessRules.CategoryCannotBeDuplicatedWhenInserted(request.CreateCategoryDtos);

                List<Category> cateogries = mapper.Map<List<Category>>(request.CreateCategoryDtos);

                var createdCategories = await categoryRepository.AddRangeAsync(cateogries);

                List<CategoryViewDto> categoryListDtos = mapper.Map<List<CategoryViewDto>>(createdCategories.ToList());

                

                return new CreateRangeCategoryResponse { CategoryViewDtos = categoryListDtos };
            }
        }
    }
}
