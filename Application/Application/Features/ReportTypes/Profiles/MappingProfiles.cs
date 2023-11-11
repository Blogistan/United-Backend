using Application.Features.Reports.Commands.DeleteReport;
using Application.Features.ReportTypes.Commands.CreateReportType;
using Application.Features.ReportTypes.Commands.DeleteReportType;
using Application.Features.ReportTypes.Commands.UpdateReportType;
using Application.Features.ReportTypes.Dtos;
using Application.Features.ReportTypes.Queries.GetListReportTypes;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ReportTypes.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReportType, CreateReportTypeCommandResponse>().ReverseMap();
            CreateMap<ReportType, CreateReportTypeCommand>().ReverseMap();


            CreateMap<ReportType, DeleteReportTypeCommandResponse>().ReverseMap();


            CreateMap<ReportType, UpdateReportTypeCommandResponse>().ReverseMap();
            CreateMap<ReportType, UpdateReportTypeCommand>().ReverseMap();

            CreateMap<ReportType, ReportTypeListViewDto>().ReverseMap();
            CreateMap<IPaginate<ReportType>, GetListReportTypeQueryResponse>().ReverseMap();

        }
    }
}
