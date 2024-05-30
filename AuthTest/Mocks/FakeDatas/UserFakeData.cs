using Core.Security.Hashing;
using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class SiteUserFakeData : BaseFakeData<SiteUser, int>
    {
        public static int[] Ids = new int[] { 234, 123, 12 };
        public override List<SiteUser> CreateFakeData()
        {
            HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);
            List<SiteUser> data = new List<SiteUser>
            {
                new SiteUser
            {
                Id = Ids[0],
                Email = "example@united.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                DeletedDate=null
            },
            new SiteUser
            {
                Id = Ids[1],
                Email = "example2@united.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                DeletedDate=null
            },
            new SiteUser
            {
                Id = Ids[2],
                Email = "example3@united.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                DeletedDate=null
            }
            };
            return data;

        }
    }
}
