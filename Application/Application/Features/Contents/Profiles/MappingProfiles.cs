using Application.Features.Contents.Dtos;
using Application.Features.Contents.Queries.GetListContent;
using Application.Features.Contents.Queries.GetListContentDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Contents.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Content, ContentListViewDto>().ReverseMap();

            CreateMap<IPaginate<Content>,GetListContentQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Content>, GetListContentDynamicQueryResponse>().ReverseMap();
        }
    }
}
