using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class ReportFakeData : BaseFakeData<Report, int>
    {
        public override List<Report> CreateFakeData()
        {
            return new List<Report>
            {
                new(){Id=1,SiteUserId=1,ReportTypeId=1,ReportDescription="TEST REPORT"},
                new(){Id=2,SiteUserId=1,ReportTypeId=2,ReportDescription="TEST REPORT2"},
                new(){Id=3,SiteUserId=1,ReportTypeId=3,ReportDescription="TEST REPORT3"}
            };
        }
    }
}
