using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class UserLoginFakeData : BaseFakeData<UserLogin, int>
    {
        public override List<UserLogin> CreateFakeData()
        {
            return new List<UserLogin>();
        }
    }
}
