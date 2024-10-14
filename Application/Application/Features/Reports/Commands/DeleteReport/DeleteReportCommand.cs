using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommand : IRequest<DeleteReportCommandResponse>,ISecuredRequest
    {
        public int ReportID { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };

        public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeleteReportCommandResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;
            private readonly ReportBusinessRules reportBusinessRules;
            public DeleteReportCommandHandler(IReportRepository reportRepository, ReportBusinessRules reportBusinessRules, IMapper mapper)
            {
                this.reportRepository = reportRepository;
                this.reportBusinessRules = reportBusinessRules;
                this.mapper = mapper;
            }

            public async Task<DeleteReportCommandResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
            {
                var report = await reportBusinessRules.ReportCheckById(request.ReportID);

                var deletedReport = await reportRepository.DeleteAsync(report,true);


                return new DeleteReportCommandResponse()
                {
                    Id = deletedReport.Id,
                    ReportDescription = deletedReport.ReportDescription,
                    ReportType = deletedReport.ReportTypeId
                };
            }
        }
    }
}
