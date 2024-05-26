using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class ForgotPasswordFakeData : BaseFakeData<ForgotPassword, int>
    {
        public override List<ForgotPassword> CreateFakeData()
        {
            return new List<ForgotPassword>() {
               new(){Id=1,UserId=123,ActivationKey="A2B2C3",IsVerified=false,ExpireDate=DateTime.UtcNow.AddDays(2)},
               new(){Id=2,UserId=123,ActivationKey="A2C6C3",IsVerified=true,ExpireDate=DateTime.UtcNow.AddDays(-1)},
               new(){Id=3,UserId=234,ActivationKey="A2B1C3",IsVerified=false,ExpireDate=DateTime.UtcNow.AddDays(-1)}
            };
        }
    }
}
