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

            CreateMap<SiteUser, SiteUserListViewDto>().ForMember(opt => opt.FirstName, src => src.MapFrom(x => x.User.FirstName))
                .ForMember(opt => opt.LastName, src => src.MapFrom(x => x.User.LastName))
                .ForMember(opt => opt.Email, src => src.MapFrom(x => x.User.Email))
                .ForMember(opt => opt.IsVerified, src => src.MapFrom(x => x.IsVerified))
                .ForMember(opt => opt.Biography, src => src.MapFrom(x => x.Biography))
                .ForMember(opt => opt.ProfileImageUrl, src => src.MapFrom(x => x.ProfileImageUrl))
                .ForMember(opt => opt.Id, src => src.MapFrom(x => x.Id))
                .ReverseMap();
            CreateMap<IPaginate<SiteUser>, GetListSiteUserQueryResponse>().ReverseMap();


        }
    }
}
