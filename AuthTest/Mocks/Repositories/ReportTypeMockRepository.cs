using Application.Features.ReportTypes.Profiles;
using Application.Features.ReportTypes.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class ReportTypeMockRepository : BaseMockRepository<IReportTypeRepository, ReportType, int, MappingProfiles, ReportTypeBusinessRules, ReportTypeFakeData, IMediator>
    {
        public ReportTypeMockRepository(ReportTypeFakeData fakeData) : base(fakeData)
        {
        }
    }
}
