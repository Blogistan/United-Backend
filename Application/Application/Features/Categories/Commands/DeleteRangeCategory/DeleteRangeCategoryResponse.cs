using Application.Features.Categories.Dtos;
using Core.Application.Responses;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public class DeleteRangeCategoryResponse:IResponse
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; }

        public DeleteRangeCategoryResponse(List<CategoryViewDto> categoryViewDtos)
        {
            CategoryViewDtos = categoryViewDtos;
        }
    }
}
