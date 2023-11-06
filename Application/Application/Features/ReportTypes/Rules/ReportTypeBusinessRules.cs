using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ReportTypes.Rules
{
    public class ReportTypeBusinessRules
    {
        private readonly IReportTypeRepository reportTypeRepository;
        public ReportTypeBusinessRules(IReportTypeRepository reportTypeRepository)
        {
            this.reportTypeRepository = reportTypeRepository;
        }
        public async Task ReportTypeCannotBeDuplicatedWhenInserted(string reporyTypeName)
        {
            ReportType reportType = await reportTypeRepository.GetAsync(x => x.ReportTypeName == reporyTypeName);
            if (reportType is not null)
                throw new BusinessException("ReportType is exists.");
        }
        public async Task ReportTypeCannotBeDuplicatedWhenUpdated(string reporyTypeName)
        {
            ReportType reportType = await reportTypeRepository.GetAsync(x => x.ReportTypeName == reporyTypeName);
            if (reportType is not null)
                throw new BusinessException("ReportType is exist");
        }

        public async Task<ReportType> ReportTypeCheckById(int id)
        {
            ReportType reportType = await reportTypeRepository.GetAsync(x => x.Id == id);
            if (reportType == null) throw new BusinessException("ReportType is not exists.");

            return reportType;
        }

    }
}
