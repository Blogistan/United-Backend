using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class VideoRepository : EfRepositoryBase<Video,int, EFDbContext>, IVideoRepository
    {
        public VideoRepository(EFDbContext context) : base(context)
        {
        }
    }
}
