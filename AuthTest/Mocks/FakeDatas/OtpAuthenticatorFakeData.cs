using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class OtpAuthenticatorFakeData : BaseFakeData<OtpAuthenticator, int>
    {
        
        public override List<OtpAuthenticator> CreateFakeData()
        {
            HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);
            return new List<OtpAuthenticator>()
            {
                new(){Id=1,UserId=234,IsVerified=false,SecretKey=passwordHash},
                new(){Id=2,UserId=123,IsVerified=false,SecretKey=passwordHash},
                new(){Id=3,UserId=12,IsVerified=false,SecretKey=passwordHash},
            };
        }
    }
}
