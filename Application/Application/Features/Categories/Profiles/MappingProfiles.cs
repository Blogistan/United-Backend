using Application.Features.Categories.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Categories.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryViewDto,Category>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x=>x.CategoryName,opt=>opt.MapFrom(src=>src.CategoryName))
                .ForMember(x => x.SubCategories, opt => opt.MapFrom(src => src.SubCategories))
                .ReverseMap();

            CreateMap<IPaginate<Category>, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryViewDto>().ReverseMap();

            CreateMap<UpdateCategoryDto,Category>().ReverseMap();
            CreateMap<CreateCategoryDto,Category>().ReverseMap();
        }
    }
}
