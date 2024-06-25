using Application.Features.Reports.Profiles;
using Application.Features.Reports.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class ReportMockRepository : BaseMockRepository<IReportRepository, Report, Guid, MappingProfiles, ReportBusinessRules, ReportFakeData, IMediator>
    {
        public ReportMockRepository(ReportFakeData fakeData) : base(fakeData)
        {
        }
    }
}
