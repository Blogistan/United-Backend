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

        public async Task<ICollection<EmailAuthenticator>> DeleteAllNonVerifiedAsync(User user)
        {
            List<EmailAuthenticator> userEmailAuthenticators = Query()
           .Where(uea => uea.UserId == user.Id && uea.IsVerified == false)
           .ToList();

            await DeleteRangeAsync(userEmailAuthenticators);
            return userEmailAuthenticators;
        }
    }
}
