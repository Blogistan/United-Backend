using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommand : IRequest<UpdateReportCommandResponse>
    {
        public Guid ReportID { get; set; }
        public string ReportDescription { get; set; }

        public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, UpdateReportCommandResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;
            private readonly ReportBusinessRules reportBusinessRules;
            public UpdateReportCommandHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
            {
                this.reportRepository = reportRepository;
                this.mapper = mapper;
                this.reportBusinessRules = reportBusinessRules;
            }

            public async Task<UpdateReportCommandResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
            {
                var report = await reportBusinessRules.ReportCheckById(request.ReportID);

                report.ReportDescription = request.ReportDescription;

                var updatedReport = await reportRepository.UpdateAsync(report);

                return new UpdateReportCommandResponse
                {
                    Id=updatedReport.Id,
                    ReportDescription=updatedReport.ReportDescription
                };
            }
        }
    }
}
