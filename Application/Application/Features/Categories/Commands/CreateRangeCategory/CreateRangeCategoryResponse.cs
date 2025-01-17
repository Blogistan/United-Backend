﻿using Application.Features.Categories.Dtos;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public record CreateRangeCategoryResponse : IRequest<CategoryListDto>, IResponse
    {
        public List<CategoryViewDto> CategoryViewDtos { get; set; }

        public CreateRangeCategoryResponse()
        {
            
        }
        public CreateRangeCategoryResponse(List<CategoryViewDto> categoryViewDtos)
        {
            CategoryViewDtos = categoryViewDtos;
        }


    }
}
