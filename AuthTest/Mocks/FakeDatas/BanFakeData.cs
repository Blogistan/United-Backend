using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class BanFakeData : BaseFakeData<Ban, Guid>
    {
        public override List<Ban> CreateFakeData()
        {
            return new List<Ban>
            {
                new(){Id=Guid.NewGuid(),UserID=123,IsPerma=true,BanDetail="Test ban",BanStartDate=DateTime.UtcNow,BanEndDate=DateTime.UtcNow.AddYears(1)},
                new(){Id=Guid.NewGuid(),UserID=234,IsPerma=true,BanDetail="Test ban",BanStartDate=DateTime.UtcNow,BanEndDate=DateTime.UtcNow.AddYears(1)}
            };
        }
    }
}
