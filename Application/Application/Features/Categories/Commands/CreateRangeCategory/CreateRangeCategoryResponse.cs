using Application.Features.Categories.Dtos;
using MediatR;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public class CreateRangeCategoryResponse:IRequest<CategoryListDto>
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; } 

        
    }
}
