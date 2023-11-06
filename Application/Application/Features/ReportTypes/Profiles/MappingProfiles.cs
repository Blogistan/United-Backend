using Application.Features.Reports.Commands.DeleteReport;
using Application.Features.ReportTypes.Commands.CreateReportType;
using Application.Features.ReportTypes.Commands.UpdateReportType;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.ReportTypes.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ReportType, CreateReportTypeCommandResponse>().ReverseMap();
            CreateMap<ReportType, CreateReportTypeCommand>().ReverseMap();


            CreateMap<ReportType, DeleteReportCommandResponse>().ReverseMap();


            CreateMap<ReportType, UpdateReportTypeCommandResponse>().ReverseMap();
            CreateMap<ReportType, UpdateReportTypeCommand>().ReverseMap();
        }
    }
}
