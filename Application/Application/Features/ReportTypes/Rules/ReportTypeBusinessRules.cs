﻿using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ReportTypes.Rules
{
    public class ReportTypeBusinessRules: BaseBusinessRules
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
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Report Type", Errors = new List<string> { "ReportType is exists." } } });
            }
        }
        public async Task ReportTypeCannotBeDuplicatedWhenUpdated(string reporyTypeName)
        {
            ReportType reportType = await reportTypeRepository.GetAsync(x => x.ReportTypeName == reporyTypeName);

            if (reportType is not null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Report Type", Errors = new List<string> { "ReportType is exists." } } });
            }

        }

        public async Task<ReportType> ReportTypeCheckById(int id)
        {
            ReportType reportType = await reportTypeRepository.GetAsync(x => x.Id == id);

            if (reportType is null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Report Type", Errors = new List<string> { "ReportType is not exists." } } });
            }

            return reportType;
        }

    }
}
