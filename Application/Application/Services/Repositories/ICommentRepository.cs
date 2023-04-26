using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ICommentRepository : IRepository<Comment,int>, IAsyncRepository<Comment,int>
    {
    }
}
