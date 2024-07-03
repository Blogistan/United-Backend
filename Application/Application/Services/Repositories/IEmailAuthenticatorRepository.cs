using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories
{
    public interface IEmailAuthenticatorRepository : IRepository<EmailAuthenticator, int>, IAsyncRepository<EmailAuthenticator, int>
    {
         Task<ICollection<EmailAuthenticator>> DeleteAllNonVerifiedAsync(User user);
    }
}
