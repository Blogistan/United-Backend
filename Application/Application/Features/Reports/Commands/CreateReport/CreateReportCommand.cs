using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommand : IRequest<CreateReportCommandResponse>
    {
        public int ReportTypeID { get; set; }
        public string ReportDescription { get; set; } = string.Empty;
        public int UserID { get; set; }

        public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;
            public CreateReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
            {
                this.reportRepository = reportRepository;
                this.mapper = mapper;
            }

            public async Task<CreateReportCommandResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
            {
                Report report = new()
                {
                    ReportTypeID = request.ReportTypeID,
                    ReportDescription = request.ReportDescription,

                };

                var createdReport = await reportRepository.AddAsync(report);

                report.UserReports.Add(new UserReport { SiteUserID = request.UserID, ReportID = report.Id });

                var reportResult = await reportRepository.GetAsync(x => x.Id == report.Id, include: x => x.Include(x => x.ReportType));

                CreateReportCommandResponse createReportCommandResponse = new()
                {
                    Id = reportResult.Id,
                    ReportType = reportResult.ReportType.ReportTypeName,
                    ReportDescription = reportResult.ReportDescription
                };

                return createReportCommandResponse;




            }
        }

    }
}
