using Application.Features.Videos.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Videos.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Video, CreateVideoDto>().ReverseMap();
            CreateMap<Video, UpdateVideoDto>().ReverseMap();

            CreateMap<Video, VideoViewDto>().ReverseMap();

            CreateMap<IPaginate<Video>, VideoListDto>().ReverseMap();

            
        }
    }
    
}
