using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class CategoryRepository : EfRepositoryBase<Category, int,EFDbContext>, ICategoryRepository
    {
        public CategoryRepository(EFDbContext context) : base(context)
        {
        }
    }
}
