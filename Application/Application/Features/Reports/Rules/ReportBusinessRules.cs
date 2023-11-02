using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Reports.Rules
{
    public class ReportBusinessRules
    {
        public readonly IReportRepository reportRepository;
        public ReportBusinessRules(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<Report> ReportCheckById(Guid reportID)
        {
            Report report = await reportRepository.GetAsync(x => x.Id == reportID);
            if (report is null)
                throw new BusinessException("Report is not exist");

            return report;
        }
    }
}
