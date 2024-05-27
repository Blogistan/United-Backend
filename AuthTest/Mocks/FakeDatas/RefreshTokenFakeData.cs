using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class RefreshTokenFakeData : BaseFakeData<RefreshToken, int>
    {
        public override List<RefreshToken> CreateFakeData()
        {
            return new List<RefreshToken> {
                new() { UserId = SiteUserFakeData.Ids[0], Token = "abc",Expires=DateTime.UtcNow.AddDays(1) },
                new() { UserId = SiteUserFakeData.Ids[1], Token = "abc1",Expires=DateTime.UtcNow.AddDays(-1)},
            };
        }
    }
}
