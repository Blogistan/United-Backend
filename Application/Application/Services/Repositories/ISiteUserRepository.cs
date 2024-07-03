using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ISiteUserRepository:IRepository<SiteUser,int>,
        IAsyncRepository<SiteUser, int>
    {
    }
}
