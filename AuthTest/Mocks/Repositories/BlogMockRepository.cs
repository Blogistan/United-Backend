using Application.Features.Blogs.Profiles;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Test.Application.Repositories;
using Domain.Entities;
using MediatR;

namespace AuthTest.Mocks.Repositories
{
    public class BlogMockRepository : BaseMockRepository<IBlogRepository, Blog, int, MappingProfiles, BlogBusinessRules, BlogFakeData, IMediator>
    {
        public BlogMockRepository(BlogFakeData fakeData) : base(fakeData)
        {
        }
    }
}
