using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class RefreshTokenFakeData : BaseFakeData<RefreshToken, int>
    {
        public override List<RefreshToken> CreateFakeData()
        {
            return new List<RefreshToken> { new() { UserId = SiteUserFakeData.Ids[0], Token = "abc" } };
        }
    }
}
