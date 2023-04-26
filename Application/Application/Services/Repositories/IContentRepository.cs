using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IContentRepository : IRepository<Content,int>, IAsyncRepository<Content,int>
    {
    }
}
