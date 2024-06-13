using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class CommentFakeData : BaseFakeData<Comment, int>
    {
        public override List<Comment> CreateFakeData()
        {
            return new List<Comment>
            {
                new(){Id=1,BlogId=1,CommentContent="TEST COMMENT",UserId=234},
                new(){Id=2,BlogId=1,CommentContent="TEST COMMENT1",UserId=123},
                new(){Id=3,BlogId=1,CommentContent="TEST COMMENT2",UserId=3},

            };
        }
    }
}
