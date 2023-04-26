using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class SiteUserRepository : EfRepositoryBase<SiteUser, int, EFDbContext>,ISiteUserRepository
    {
        public SiteUserRepository(EFDbContext context) : base(context)
        {
        }
    }
}
