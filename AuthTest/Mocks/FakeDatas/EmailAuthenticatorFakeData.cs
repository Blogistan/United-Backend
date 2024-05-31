using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class EmailAuthenticatorFakeData : BaseFakeData<EmailAuthenticator, int>
    {
        public override List<EmailAuthenticator> CreateFakeData()
        {
            return new List<EmailAuthenticator> {
                new() {Id=1,UserId=234,IsVerified=true,ActivationKey="T123" },
                new() {Id=2,UserId=123,IsVerified=false,ActivationKey="T111" }
                };
        }
    }
}
