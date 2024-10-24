using Application.Features.Categories.Dtos;
using Core.Application.Responses;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public record DeleteRangeCategoryResponse :IResponse
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; }

        public DeleteRangeCategoryResponse()
        {
            
        }
        public DeleteRangeCategoryResponse(List<CategoryViewDto> categoryViewDtos)
        {
            CategoryViewDtos = categoryViewDtos;
        }
    }
}
