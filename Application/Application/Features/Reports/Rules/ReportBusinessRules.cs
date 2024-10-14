using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Reports.Rules
{
    public class ReportBusinessRules: BaseBusinessRules
    {
        public readonly IReportRepository reportRepository;
        public ReportBusinessRules(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public async Task<Report> ReportCheckById(int reportID)
        {
            Report report = await reportRepository.GetAsync(x => x.Id == reportID);
            if (report is null)
                throw new NotFoundException("Report is not exist");

            return report;
        }
    }
}
