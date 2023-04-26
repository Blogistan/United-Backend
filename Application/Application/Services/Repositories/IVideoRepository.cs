using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IVideoRepository : IAsyncRepository<Video,int>, IRepository<Video,int>
    {
    }
}
