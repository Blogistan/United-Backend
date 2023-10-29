using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IUserBanRepository:IRepository<UserBan,Guid>,IAsyncRepository<UserBan,Guid>
    {
    }
}
