using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;

namespace Application.Features.Auth.Rules
{
    public class AuthBussinessRules
    {
        private readonly ISiteUserRepository siteUserRepository;
        public AuthBussinessRules(ISiteUserRepository siteUserRepository)
        {
            this.siteUserRepository = siteUserRepository;
        }
        public async Task UserEmailCannotBeDuplicatedWhenInserted(string email)
        {
            User? user = await siteUserRepository.GetAsync(x => x.Email == email);
            if (user != null) throw new BusinessException(AuthBusinessMessage.UserEmailAlreadyExists);

        }
        public Task UserShouldBeExist(User? user)
        {
            if (user == null)
                throw new BusinessException(AuthBusinessMessage.UserNotFound);

            return Task.CompletedTask;
        }
        public Task UserPasswordShoudBeMatch(User user, string password)
        {
            bool result = HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (result == false)
                throw new BusinessException(AuthBusinessMessage.InvlaidPassword);

            return Task.CompletedTask;
        }
        public Task RefreshTokenShouldBeExist(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new BusinessException(AuthBusinessMessage.RefreshTokenNotFound);

            return Task.CompletedTask;
        }
        public  Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null || refreshToken.Revoked == null && refreshToken.Expires < DateTime.UtcNow)
                throw new BusinessException(AuthBusinessMessage.RefreshTokenNotActive);

            return Task.CompletedTask;
        }
        public Task UserShouldNotBeHasAuthenticator(User user)
        {
            if (user.AuthenticatorType is not AuthenticatorType.None)
                throw new BusinessException(AuthBusinessMessage.UserHasAuthenticator);
            return Task.CompletedTask;
        }

        public Task UserEmailAuthenticatorShouldBeExists(EmailAuthenticator? userEmailAuthenticator)
        {
            if (userEmailAuthenticator is null)
                throw new BusinessException(AuthBusinessMessage.UserEmailAuthenticatorNotFound);
            return Task.CompletedTask;
        }

        public Task UserOtpAuthenticatorShouldBeExists(OtpAuthenticator userOtpAuthenticator)
        {
            if (userOtpAuthenticator is null)
                throw new BusinessException(AuthBusinessMessage.UserOtpAuthenticatorNotFound);
            return Task.CompletedTask;
        }

    }
}
