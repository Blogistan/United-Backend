using Core.Test.Application.FakeData;
using Domain.Entities;

namespace AuthTest.Mocks.FakeDatas
{
    public class CategoryFakeData : BaseFakeData<Category, int>
    {
        public override List<Category> CreateFakeData()
        {
            return new List<Category>
            {
                new(){Id=1,CategoryName="TestCat1"},
                new(){Id=2,CategoryName="TestCat2"},
                new(){Id=3,CategoryName="TestCat3"},
            };
        }
    }
}
