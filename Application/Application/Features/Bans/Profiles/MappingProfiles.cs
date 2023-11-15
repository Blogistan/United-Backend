using Application.Features.Bans.Commands.CreateBan;
using Application.Features.Bans.Commands.DeleteBan;
using Application.Features.Bans.Commands.UpdateBan;
using Application.Features.Bans.Dtos;
using Application.Features.Bans.Queries.GetListBans;
using Application.Features.Bans.Queries.GetListBansDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Bans.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ban, BanListViewDto>().ForMember(opt => opt.Id, src => src.MapFrom(src => src.Id))
                .ForMember(opt => opt.UserName, src => src.MapFrom(src => src.User.FirstName + ' ' + src.User.LastName))
                .ForMember(opt => opt.IsPerma, src => src.MapFrom(src => src.IsPerma))
                .ForMember(opt => opt.BanStartDate, src => src.MapFrom(src => src.BanStartDate))
                .ForMember(opt => opt.BanEndDate, src => src.MapFrom(src => src.BanEndDate))
                .ForMember(opt => opt.BanDetail, src => src.MapFrom(src => src.BanDetail))
                .ReverseMap();

            CreateMap<IPaginate<Ban>, GetListBansQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Ban>, GetListBansDynamicQueryResponse>().ReverseMap();

            CreateMap<Ban, CreateBanCommand>().ReverseMap();
            CreateMap<Ban, CreateBanCommandResponse>().ForMember(opt => opt.Id, src => src.MapFrom(src => src.Id))
                .ForMember(opt => opt.UserName, src => src.MapFrom(src => src.User.FirstName + ' ' + src.User.LastName))
                .ForMember(opt => opt.IsPerma, src => src.MapFrom(src => src.IsPerma))
                .ForMember(opt => opt.BanStartDate, src => src.MapFrom(src => src.BanStartDate))
                .ForMember(opt => opt.BanEndDate, src => src.MapFrom(src => src.BanEndDate))
                .ForMember(opt => opt.BanDetail, src => src.MapFrom(src => src.BanDetail))
                .ReverseMap();


            CreateMap<Ban, DeleteBanCommandResponse>().ForMember(opt => opt.Id, src => src.MapFrom(src => src.Id))
               .ForMember(opt => opt.UserName, src => src.MapFrom(src => src.User.FirstName + ' ' + src.User.LastName))
               .ForMember(opt => opt.IsPerma, src => src.MapFrom(src => src.IsPerma))
               .ForMember(opt => opt.BanStartDate, src => src.MapFrom(src => src.BanStartDate))
               .ForMember(opt => opt.BanEndDate, src => src.MapFrom(src => src.BanEndDate))
               .ForMember(opt => opt.BanDetail, src => src.MapFrom(src => src.BanDetail))
               .ReverseMap();

            CreateMap<Ban, UpdateBanCommand>().ForMember(opt => opt.Id, src => src.MapFrom(src => src.Id))
                .ForMember(opt => opt.ReportID, src => src.MapFrom(src => src.ReportID))
               .ForMember(opt => opt.UserId, src => src.MapFrom(src => src.UserID))
               .ForMember(opt => opt.IsPerma, src => src.MapFrom(src => src.IsPerma))
               .ForMember(opt => opt.BanStartDate, src => src.MapFrom(src => src.BanStartDate))
               .ForMember(opt => opt.BanEndDate, src => src.MapFrom(src => src.BanEndDate))
               .ForMember(opt => opt.BanDetail, src => src.MapFrom(src => src.BanDetail))
                .ReverseMap();
            CreateMap<Ban, UpdateBanCommandResponse>().ReverseMap();
        }
    }
}
