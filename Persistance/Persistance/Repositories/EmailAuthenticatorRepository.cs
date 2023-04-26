using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class EmailAuthenticatorRepository : EfRepositoryBase<EmailAuthenticator, int,EFDbContext>, IEmailAuthenticatorRepository
    {
        public EmailAuthenticatorRepository(EFDbContext context) : base(context)
        {
        }
    }
}
