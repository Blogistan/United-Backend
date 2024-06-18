using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class ContentFakeData : BaseFakeData<Content, int>
    {
        public override List<Content> CreateFakeData()
        {
            return new List<Content>
            {
                new(){Id=1,Title="Test Content",ContentPragraph="ASELOGJSDIOGHSDUPIGHIUAPQGBHUYIAQPHGUŞKASDBHNGŞOUISDN",CreateUser=1,CreatedDate=DateTime.UtcNow,DeletedDate=null},
                new(){Id=2,Title="Test Content2",ContentPragraph="asd",CreateUser=1,CreatedDate=DateTime.UtcNow,DeletedDate=null},
                new(){Id=3,Title="Test Content3",ContentPragraph="dgbfdhfghgfhgf",CreateUser=1,CreatedDate=DateTime.UtcNow,DeletedDate=null},


            };
        }
    }
}
