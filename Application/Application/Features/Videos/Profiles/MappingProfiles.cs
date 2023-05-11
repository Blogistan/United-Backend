using Application.Features.Categories.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Videos.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryViewDto>().ReverseMap();

            CreateMap<IPaginate<CategoryListDto>, CategoryListDto>().ReverseMap();
        }
    }
}
