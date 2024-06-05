using Application.Features.Categories.Profiles;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class CategoryMockRepository : BaseMockRepository<ICategoryRepository, Category, int, MappingProfiles, CategoryBusinessRules, CategoryFakeData, IMediator>
    {
        public CategoryMockRepository(CategoryFakeData fakeData) : base(fakeData)
        {
        }
    }
}
