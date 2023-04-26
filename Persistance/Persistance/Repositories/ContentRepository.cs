using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ContentRepository : EfRepositoryBase<Content, int,EFDbContext>, IContentRepository
    {
        public ContentRepository(EFDbContext context) : base(context)
        {
        }
    }
}
