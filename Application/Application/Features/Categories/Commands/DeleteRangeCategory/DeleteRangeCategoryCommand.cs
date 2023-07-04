using Application.Features.Categories.Dtos;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public class DeleteRangeCategoryCommand : IRequest<DeleteRangeCategoryResponse>,ISecuredRequest
    {
        public List<DeleteRangeCategoryDto> DeleteRangeCategoryDtos { get; set; }
        public bool Permanent { get; set; }
        public string[] Roles => new string[] { "Admin", "Moderator" };

        public class DeleteRangeCategoryCommandHandler : IRequestHandler<DeleteRangeCategoryCommand, DeleteRangeCategoryResponse>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly IMapper mapper;
            private readonly CategoryBusinessRules categoryBusinessRules;

            public DeleteRangeCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
            {
                this.categoryRepository = categoryRepository;
                this.mapper = mapper;
                this.categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<DeleteRangeCategoryResponse> Handle(DeleteRangeCategoryCommand request, CancellationToken cancellationToken)
            {
                var categories = await categoryBusinessRules.CategoryCheckById(request.DeleteRangeCategoryDtos);

                ICollection<Category> deletedCategories = await categoryRepository.DeleteRangeAsync(categories, request.Permanent);

                List<CategoryViewDto> categoryViewDtos = mapper.Map<List<CategoryViewDto>>(deletedCategories);

                return new DeleteRangeCategoryResponse { CategoryViewDtos = categoryViewDtos };

            }
        }
    }
}
