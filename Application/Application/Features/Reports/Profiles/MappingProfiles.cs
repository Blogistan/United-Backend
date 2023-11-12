using Application.Features.Reports.Dtos;
using Application.Features.Reports.Queries.GetListReport;
using Application.Features.Reports.Queries.GetListReportDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Reports.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Report, ReportListViewDto>().ForMember(opt=>opt.ReportType,src=>src.MapFrom(x=>x.ReportType.ReportTypeName))
                .ForMember(opt=>opt.UserName,src=>src.MapFrom(x=>x.User.FirstName+' '+x.User.LastName)).ReverseMap();

            CreateMap<IPaginate<Report>, GetListReportQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Report>, GetListReportDynamicQueryResponse>().ReverseMap();
        }
    }
}
