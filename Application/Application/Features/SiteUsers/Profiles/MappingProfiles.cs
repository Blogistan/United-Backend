using Application.Features.SiteUsers.Commands.CreateSiteUser;
using Application.Features.SiteUsers.Commands.DeleteSiteUser;
using Application.Features.SiteUsers.Commands.UpdateSiteUser;
using Application.Features.SiteUsers.Dtos;
using Application.Features.SiteUsers.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.SiteUsers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SiteUser, CreateSiteUserCommand>().ReverseMap();
            CreateMap<SiteUser, CreateSiteUserResponse>().ReverseMap();

            CreateMap<SiteUser, DeleteSiteUserCommandResponse>().ForMember(opt => opt.Id, src => src.MapFrom(x => x.Id)).ReverseMap();

            CreateMap<SiteUser, UpdateSiteUserCommand>().ReverseMap();
            CreateMap<SiteUser, UpdateSiteUserCommandResponse>().ReverseMap();

            CreateMap<SiteUser, SiteUserListViewDto>().ReverseMap();
            CreateMap<IPaginate<SiteUser>, GetListSiteUserQueryResponse>().ReverseMap();
        }
    }
}
