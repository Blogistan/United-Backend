using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class BlogFakeData : BaseFakeData<Blog, int>
    {
        public override List<Blog> CreateFakeData()
        {
            return
            [
                new(){Id=1,CategoryId=1,WriterId=1,ContentId=1,Title="Test Blog",BannerImageUrl="Img1"},
                new(){Id=2,CategoryId=2,WriterId=2,ContentId=2,Title="Test Blog2",BannerImageUrl="Img2"},
                new(){Id=3,CategoryId=3,WriterId=3,ContentId=3,Title="Test Blog3",BannerImageUrl="Img3"},
            ];
        }
    }
}
