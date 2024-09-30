using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Rules
{
    public class AuthBussinessRules : BaseBusinessRules
    {
        private readonly IUserRepository userRepository;
        private readonly ISiteUserRepository siteUserRepository;
        public AuthBussinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task UserEmailCannotBeDuplicatedWhenInserted(string email)
        {
            User? user = await userRepository.GetAsync(x => x.Email == email);
            if (user != null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Email", Errors = new List<string> { AuthBusinessMessage.UserEmailAlreadyExists } } });
            }

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
                throw new AuthorizationException(AuthBusinessMessage.InvlaidPassword);

            return Task.CompletedTask;
        }
        public Task RefreshTokenShouldBeExist(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new AuthorizationException(AuthBusinessMessage.RefreshTokenNotFound);

            return Task.CompletedTask;
        }
        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null || refreshToken.Revoked == null && refreshToken.Expires < DateTime.UtcNow)
                throw new AuthorizationException(AuthBusinessMessage.RefreshTokenNotActive);

            return Task.CompletedTask;
        }
        public Task UserShouldNotBeHasAuthenticator(User user)
        {
            if (user.AuthenticatorType is not AuthenticatorType.None)
                throw new AuthorizationException(AuthBusinessMessage.UserHasAuthenticator);
            return Task.CompletedTask;
        }

        public Task UserEmailAuthenticatorShouldBeExists(EmailAuthenticator? userEmailAuthenticator)
        {
            if (userEmailAuthenticator is null)
                throw new AuthorizationException(AuthBusinessMessage.UserEmailAuthenticatorNotFound);
            return Task.CompletedTask;
        }

        public Task UserOtpAuthenticatorShouldBeExists(OtpAuthenticator userOtpAuthenticator)
        {
            if (userOtpAuthenticator is null)
                throw new AuthorizationException(AuthBusinessMessage.UserOtpAuthenticatorNotFound);
            return Task.CompletedTask;
        }
        public Task PasswordResetKeyShouldBeExists(ForgotPassword forgotPassword)
        {
            if (forgotPassword is null)
                throw new AuthorizationException("Invlaid Reset Token");
            return Task.CompletedTask;
        }
        public Task PasswordResetTokenShouldBeActive(ForgotPassword forgotPassword)
        {
            if (forgotPassword.ExpireDate < DateTime.UtcNow)
                throw new AuthorizationException("Password Reset Token not active");

            return Task.CompletedTask;
        }
        public async Task IsUserActive(int id)
        {
            var user = await siteUserRepository.GetAsync(x => x.Id == id && x.User.IsActive == false, include: x => x.Include(x => x.Bans).Include(x=>x.User));

            var result = user?.Bans?.Any(x => x.IsPerma == true) ?? false;
            if (result)
                throw new AuthorizationException(AuthBusinessMessage.UserPermaBanned);
        }
        public async Task IsUserTimeOut(int id)
        {
            var user = await siteUserRepository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.Bans));

            var activeBan = user?.Bans?.FirstOrDefault(x => x.BanStartDate <= DateTime.Now && x.BanEndDate >= DateTime.Now);
            if (activeBan != null)
            {
                var daysUntilEnd = (int)(activeBan.BanEndDate - DateTime.Now).TotalDays;
                throw new AuthorizationException($"Your account is banned , your ban ends  at {daysUntilEnd} days");
            }
        }

    }
}
