using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ForgotPasswordRepository : EfRepositoryBase<ForgotPassword, int, EFDbContext>, IForgotPasswordRepository
    {
        public ForgotPasswordRepository(EFDbContext context) : base(context)
        {
        }


    }
}
