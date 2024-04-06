using Application.Features.Categories.Dtos;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public class CreateRangeCategoryResponse : IRequest<CategoryListDto>, IResponse
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; }

        public CreateRangeCategoryResponse(List<CategoryViewDto> categoryViewDtos)
        {
            CategoryViewDtos = categoryViewDtos;
        }


    }
}
