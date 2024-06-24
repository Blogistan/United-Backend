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
                new(){Id=1,ReportTypeName="Rahatsız Edici Söylem"},
                new(){Id=2,ReportTypeName="Aşağılama"},
                new(){Id=3,ReportTypeName="Taciz"},
            ];
        }
    }
}
