using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.SiteUsers.Rules
{
    public class UserBusinessRules : BaseBusinessRules
    {
        private readonly ISiteUserRepository siteUserRepository;
        public UserBusinessRules(ISiteUserRepository siteUserRepository)
        {
            this.siteUserRepository = siteUserRepository;
        }
        public async Task UserShouldBeExistsWhenSelected(User? user)
        {
            if (user == null)
                throw new BusinessException(AuthBusinessMessage.UserNotFound);
        }

        public async Task UserIdShouldBeExistsWhenSelected(int id)
        {
            bool doesExist = await siteUserRepository.AnyAsync(predicate: u => u.Id == id);
            if (doesExist)
                throw new BusinessException(AuthBusinessMessage.UserNotFound);
        }

        public async Task UserPasswordShouldBeMatched(User user, string password)
        {
            if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException(AuthBusinessMessage.InvlaidPassword);
        }

        public async Task UserEmailShouldNotExistsWhenInsert(string email)
        {
            bool doesExists = await siteUserRepository.AnyAsync(predicate: u => u.Email == email);
            if (doesExists)
                throw new BusinessException(AuthBusinessMessage.UserEmailAlreadyExists);
        }

        public async Task UserEmailShouldNotExistsWhenUpdate(int id, string email)
        {
            bool doesExists = await siteUserRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email);
            if (doesExists)
                throw new BusinessException(AuthBusinessMessage.UserNotFound);
        }
    }
}
