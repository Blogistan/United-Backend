﻿using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Rules
{
    public class AuthBussinessRules:BaseBusinessRules
    {
        private readonly ISiteUserRepository siteUserRepository;
        public AuthBussinessRules(ISiteUserRepository siteUserRepository)
        {
            this.siteUserRepository = siteUserRepository;
        }
        public async Task UserEmailCannotBeDuplicatedWhenInserted(string email)
        {
            UserBase? user = await siteUserRepository.GetAsync(x => x.Email == email);
            if (user != null) throw new BusinessException(AuthBusinessMessage.UserEmailAlreadyExists);

        }
        public Task UserShouldBeExist(UserBase? user)
        {
            if (user == null)
                throw new BusinessException(AuthBusinessMessage.UserNotFound);

            return Task.CompletedTask;
        }
        public Task UserPasswordShoudBeMatch(UserBase user, string password)
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
        public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
        {
            if (refreshToken.Revoked != null || refreshToken.Revoked == null && refreshToken.Expires < DateTime.UtcNow)
                throw new BusinessException(AuthBusinessMessage.RefreshTokenNotActive);

            return Task.CompletedTask;
        }
        public Task UserShouldNotBeHasAuthenticator(UserBase user)
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
        public Task PasswordResetKeyShouldBeExists(ForgotPassword forgotPassword)
        {
            if (forgotPassword is null)
                throw new BusinessException("Invlaid Reset Token");
            return Task.CompletedTask;
        }
        public Task PasswordResetTokenShouldBeActive(ForgotPassword forgotPassword)
        {
            if (forgotPassword.ExpireDate < DateTime.UtcNow)
                throw new BusinessException("Password Reset Token not active");

            return Task.CompletedTask;
        }
        public async Task IsUserActive(int id)
        {
            var user = await siteUserRepository.GetAsync(x => x.Id == id && x.IsActive==false, include: x => x.Include(x => x.Bans));

            var result = user?.Bans?.Any(x=>x.IsPerma==true)?? false;
            if (result)
                throw new BusinessException(AuthBusinessMessage.UserPermaBanned);
        }
        public async Task IsUserTimeOut(int id)
        {
            var user = await siteUserRepository.GetAsync(x => x.Id == id , include: x => x.Include(x => x.Bans));

            var activeBan = user?.Bans?.FirstOrDefault(x => x.BanStartDate <= DateTime.Now && x.BanEndDate >= DateTime.Now);
            if (activeBan!=null)
            {
                var daysUntilEnd = (int)(activeBan.BanEndDate - DateTime.Now).TotalDays;
                throw new BusinessException($"Your account is banned , your ban ends  at {daysUntilEnd} days");
            }
        }

    }
}
