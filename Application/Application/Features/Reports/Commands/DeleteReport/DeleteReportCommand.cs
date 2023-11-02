using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommand : IRequest<DeleteReportCommandResponse>
    {
        public Guid ReportID { get; set; }


        public class DeleteReportCommandhandler : IRequestHandler<DeleteReportCommand, DeleteReportCommandResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;
            private readonly ReportBusinessRules reportBusinessRules;
            public DeleteReportCommandhandler(IReportRepository reportRepository, ReportBusinessRules reportBusinessRules, IMapper mapper)
            {
                this.reportRepository = reportRepository;
                this.reportBusinessRules = reportBusinessRules;
                this.mapper = mapper;
            }

            public async Task<DeleteReportCommandResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
            {
                var report = await reportBusinessRules.ReportCheckById(request.ReportID);

                var deletedReport = await reportRepository.DeleteAsync(report);

                return new DeleteReportCommandResponse
                {
                    Id = report.Id,
                    ReportDescription = deletedReport.ReportDescription,
                    ReportType = deletedReport.ReportType.ReportTypeName
                };
            }
        }
    }
}
