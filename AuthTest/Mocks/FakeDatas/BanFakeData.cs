using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class BanFakeData : BaseFakeData<Ban, int>
    {
        public override List<Ban> CreateFakeData()
        {
            return new List<Ban>
            {
                new(){Id=1,SiteUserId=123,IsPerma=true,BanDetail="Test ban",BanStartDate=DateTime.UtcNow,BanEndDate=DateTime.UtcNow.AddYears(1)},
                new(){Id=2,SiteUserId=234,IsPerma=false,BanDetail="Test ban",BanStartDate=DateTime.UtcNow,BanEndDate=DateTime.UtcNow.AddYears(1)}
            };
        }
    }
}
