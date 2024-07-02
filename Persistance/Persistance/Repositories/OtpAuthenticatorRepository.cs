using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, int, EFDbContext>, IOtpAuthenticatorRepository
    {
        public OtpAuthenticatorRepository(EFDbContext context) : base(context)
        {
        }

        public async Task<ICollection<OtpAuthenticator>> DeleteAllNonVerifiedAsync(UserBase user)
        {
            List<OtpAuthenticator> otpAuthenticators = Query().Where(x => x.IsVerified == false && x.UserId == user.Id).ToList();

            await DeleteRangeAsync(otpAuthenticators);

            return otpAuthenticators;
        }
    }
}
