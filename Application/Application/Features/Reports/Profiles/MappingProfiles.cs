using Application.Features.Reports.Dtos;
using Application.Features.Reports.Queries.GetListReport;
using Application.Features.Reports.Queries.GetListReportDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Reports.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Report, ReportListViewDto>()
                .ForMember(opt => opt.ReportType, src => src.MapFrom(x => x.ReportType.ReportTypeName))
                .ForPath(opt => opt.User.UserName, src => src.MapFrom(x => x.SiteUser.User.FirstName + ' ' + x.SiteUser.User.LastName))
                .ForPath(opt => opt.User.UserEmail, src => src.MapFrom(x => x.SiteUser.User.Email))
                 .ForPath(opt => opt.User.UserId, src => src.MapFrom(x => x.SiteUser.User.Id))
                    .ReverseMap();


            CreateMap<IPaginate<Report>, GetListReportQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Report>, GetListReportDynamicQueryResponse>().ReverseMap();
        }
    }
}
