using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class ReportTypeFakeData : BaseFakeData<ReportType, int>
    {
        public override List<ReportType> CreateFakeData()
        {
            return
            [
                new(){Id=1,ReportTypeName="Rahatsız Edici Söylem",ReportTypeDescription="Test"},
                new(){Id=2,ReportTypeName="Aşağılama",ReportTypeDescription="Test1"},
                new(){Id=3,ReportTypeName="Taciz",ReportTypeDescription="Test2"},
            ];
        }
    }
}
