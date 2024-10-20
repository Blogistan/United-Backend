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
                .ForPath(opt => opt.User.UserName, src => src.MapFrom(x => x.SiteUser.User.FirstName + ' ' + x.SiteUser.User.LastName))
                .ForPath(opt => opt.User.UserEmail, src => src.MapFrom(x => x.SiteUser.User.Email))
                 .ForPath(opt => opt.User.UserId, src => src.MapFrom(x => x.SiteUser.Id))
                 .ForPath(opt => opt.ReportType.Id, src => src.MapFrom(x => x.ReportType.Id))
                .ForPath(opt => opt.ReportType.ReportTypeName, src => src.MapFrom(x => x.ReportType.ReportTypeName))
                 .ForPath(opt => opt.ReportType.ReportTypeDescription, src => src.MapFrom(x => x.ReportType.ReportTypeDescription))
                    .ReverseMap();


            CreateMap<IPaginate<Report>, GetListReportQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Report>, GetListReportDynamicQueryResponse>().ReverseMap();
        }
    }
}
