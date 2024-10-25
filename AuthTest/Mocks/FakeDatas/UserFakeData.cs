using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Test.Application.FakeData;

namespace AuthTest.Mocks.FakeDatas
{
    public class UserFakeData : BaseFakeData<User, int>
    {
        public static int[] Ids = new int[] { 234, 123, 12 };
        public override List<User> CreateFakeData()
        {
            HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);
            List<User> data = new List<User>
            {
                new User
            {
                Id = Ids[0],
                CreatedDate = DateTime.Now,
                DeletedDate=null
            },
            new User
            {
                Id = Ids[1],
                CreatedDate = DateTime.Now,
                DeletedDate=null,

            },
            new User
            {
                Id = Ids[2],
                CreatedDate = DateTime.Now,
                DeletedDate=null,
            }
            };
            return data;

        }
    }
}
