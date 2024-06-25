using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class ReportFakeData : BaseFakeData<Report, Guid>
    {
        public override List<Report> CreateFakeData()
        {
            return new List<Report>
            {
                new(){Id=Guid.NewGuid(),UserID=1,ReportTypeID=1,ReportDescription="TEST REPORT"},
                new(){Id=Guid.NewGuid(),UserID=1,ReportTypeID=2,ReportDescription="TEST REPORT2"},
                new(){Id=Guid.NewGuid(),UserID=1,ReportTypeID=3,ReportDescription="TEST REPORT3"}
            };
        }
    }
}
