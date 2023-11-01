using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class BanRepository : EfRepositoryBase<Ban, Guid, EFDbContext>, IBanRepository
    {
        public BanRepository(EFDbContext context) : base(context)
        {

        }

    }
}
