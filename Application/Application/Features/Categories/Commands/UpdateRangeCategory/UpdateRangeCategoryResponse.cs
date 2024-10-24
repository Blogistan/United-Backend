using Application.Features.Categories.Dtos;
using Core.Application.Responses;

namespace Application.Features.Categories.Commands.UpdateRangeCategory
{
    public record UpdateRangeCategoryResponse :IResponse
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; }

        public UpdateRangeCategoryResponse()
        {
            
        }
        public UpdateRangeCategoryResponse(List<CategoryViewDto> categoryViewDtos)
        {
            CategoryViewDtos = categoryViewDtos;
        }
    }
}
