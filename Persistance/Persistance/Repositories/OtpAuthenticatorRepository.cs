using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, int,EFDbContext>, IOtpAuthenticatorRepository
    {
        public OtpAuthenticatorRepository(EFDbContext context) : base(context)
        {
        }
    }
}
