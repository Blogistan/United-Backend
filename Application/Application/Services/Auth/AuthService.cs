﻿using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Persistence.Paging;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Domain.Entities;
using Google.Apis.Auth;
using Infrastructure.Dtos;
using Infrastructure.Dtos.Facebook;
using Infrastructure.Dtos.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Text;
using System.Text.Json;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHelper tokenHelper;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly ISiteUserRepository siteUserRepository;
        private readonly IEmailAuthenticatorRepository emailAuthenticatorRepository;
        private readonly IUserOperationClaimRepository userOperationClaimRepository;
        private readonly IMailService mailService;
        private IOtpAuthenticatorHelper otpAuthenticatorHelper;
        private readonly IEmailAuthenticatorHelper emailAuthenticatorHelper;
        private readonly IOtpAuthenticatorRepository otpAuthenticatorRepository;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AuthService(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, ISiteUserRepository siteUserRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IUserOperationClaimRepository userOperationClaimRepository, IMailService mailService,
            IOtpAuthenticatorHelper otpAuthenticatorHelper,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            IOtpAuthenticatorRepository otpAuthenticatorRepository, HttpClient httpClient, IConfiguration configuration)
        {
            this.tokenHelper = tokenHelper;
            this.mailService = mailService;
            this.refreshTokenRepository = refreshTokenRepository;
            this.siteUserRepository = siteUserRepository;
            this.emailAuthenticatorRepository = emailAuthenticatorRepository;
            this.userOperationClaimRepository = userOperationClaimRepository;
            this.otpAuthenticatorHelper = otpAuthenticatorHelper;
            this.otpAuthenticatorRepository = otpAuthenticatorRepository;
            this.emailAuthenticatorHelper = emailAuthenticatorHelper;
            this.httpClient = httpClient;
            this.configuration = configuration;
        }



        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await refreshTokenRepository.AddAsync(refreshToken);

            return addedRefreshToken;
        }

        public Task<string> ConvertOtpKeyToString(byte[] secretBtyes)
        {
            return otpAuthenticatorHelper.ConvertSecretKeyToString(secretBtyes);
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id, include: x => x.Include(inc => inc.OperationClaim));

            List<OperationClaim> userClaims = paginate.Items.Select(x => new OperationClaim { Id = x.Id, Name = x.OperationClaim.Name }).ToList();

            AccessToken accessToken = tokenHelper.CreateToken(user, userClaims);
            return accessToken;

        }

        public async Task<EmailAuthenticator> CreateEmailAutenticator(User user)
        {
            return new EmailAuthenticator
            {
                UserId = user.Id,
                ActivationKey = await emailAuthenticatorHelper.CreateEmailActivationKey(),
                IsVerified = false
            };
        }

        public async Task<RefreshToken> CreateRefreshToken(User user, string IpAddress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(user, IpAddress);

            return refreshToken;
        }

        public async Task DeleteOldActiveRefreshTokens(User user)
        {
            ICollection<RefreshToken> oldActiveTokens = await refreshTokenRepository.GetAllOldActiveRefreshTokenAsync(user, tokenHelper.RefreshTokenTTLOption);

            await refreshTokenRepository.DeleteRangeAsync(oldActiveTokens.ToList());
        }

        public async Task RevokeDescendantRefreshTokens(RefreshToken token, string IpAddress, string reason)
        {
            RefreshToken childRefreshToken = (await refreshTokenRepository.GetAsync(rt => token.ReplacedByToken == rt.Token))!;
            if (childRefreshToken == null) throw new BusinessException(AuthBusinessMessage.CouldntFindChildToken);


            if (childRefreshToken.Revoked == null) await RevokeRefreshToken(childRefreshToken, IpAddress, reason, null);
            else await RevokeDescendantRefreshTokens(childRefreshToken, IpAddress, reason);
        }

        public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
        {
            RefreshToken newToken = tokenHelper.CreateRefreshToken(user, ipAddress);
            await RevokeRefreshToken(refreshToken, ipAddress, "New Refresh Token Created. ", newToken.Token);
            await AddRefreshToken(newToken);
            return newToken;
        }

        public async Task SendAuthenticatorCode(User user)
        {
            switch (user.AuthenticatorType)
            {

                case Core.Security.Enums.AuthenticatorType.Email:
                    await SendAuthenticatorCodeWithEmail(user);
                    break;
            }
        }
        //This method could be updated.
        private async Task SendAuthenticatorCodeWithEmail(User user)
        {
            EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);

            string authCode = await emailAuthenticatorHelper.CreateEmailActivationCode();
            emailAuthenticator.ActivationKey = authCode;

            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);


            List<MailboxAddress> mailboxAddresses = new()
            {
                new MailboxAddress(Encoding.UTF8,user.FirstName,user.Email)
            };

            Mail mail = new()
            {
                ToList = mailboxAddresses,
                Subject = AuthBusinessMessage.AuthenticatorCodeSubject,
                TextBody = AuthBusinessMessage.AuthenticatorCodeTextBody(authCode)
            };

            await mailService.SendEmailAsync(mail);


        }

        public async Task VerifyAuthenticatorCode(User user, string code)
        {
            switch (user.AuthenticatorType)
            {

                case Core.Security.Enums.AuthenticatorType.Email:
                    await VerifyEmailAuthenticatorCode(user, code);
                    break;
                case Core.Security.Enums.AuthenticatorType.Otp:
                    await VerifyEmailOtpAuthenticatorCode(user, code);
                    break;

            }
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken, string IpAddress, string reason, string? replacedByToken)
        {
            refreshToken.RevokedByIp = IpAddress;
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;
            await refreshTokenRepository.UpdateAsync(refreshToken);
        }
        private async Task VerifyEmailAuthenticatorCode(User user, string code)
        {
            EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);

            if (emailAuthenticator.ActivationKey != code)
                throw new BusinessException(AuthBusinessMessage.InvalidAuthenticatorCode);
            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
        private async Task VerifyEmailOtpAuthenticatorCode(User user, string codeToVerify)
        {
            OtpAuthenticator otpAuthenticator = await otpAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);
            bool result = await otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, codeToVerify);
            if (!result)
                throw new BusinessException(AuthBusinessMessage.InvalidAuthenticatorCode); ;

        }

        public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user) => new()
        {
            UserId = user.Id,
            SecretKey = await otpAuthenticatorHelper.GenerateSecretKey(),
            IsVerified = false,
        };

        public async Task<LoginResponseBase> CreateUserExternalAsync(SiteUser user, string email, string name, string surname, string picture, string ipAdress)
        {
            bool result = user != null;

            AccessToken accessToken = new();
            RefreshToken refreshToken = new();

            GoogleLoginResponse loginResponse = new();

            if (user == null)
            {
                SiteUser siteUser = new()
                {
                    FirstName = name,
                    LastName = surname,
                    Email = email,
                    Status = true,
                    ProfileImageUrl = picture
                };

                var createdUser = await siteUserRepository.AddAsync(siteUser);
                accessToken = await CreateAccessToken(siteUser);
                refreshToken = await CreateRefreshToken(siteUser, ipAdress);

                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
            }
            else
            {
                accessToken = await CreateAccessToken(user);
                refreshToken = await CreateRefreshToken(user, ipAdress);
                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
            }

            return loginResponse;
        }

        public async Task<GoogleJsonWebSignature.Payload> GoogleSignIn(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { configuration["Google:web:client_id"] }
            };

            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        }

        public async Task<FacebookUserInfoResponse> FacebookSignIn(string authToken)
        {
            string accesTokenResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={configuration["Authentication:Facebook:AppId"]}&client_secret={configuration["Authentication:Facebook:AppSecret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accesTokenResponse);
            
            string userAccessTokenValidation = await httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            FacebookUserInfoResponse? userInfoResponse = new();

            if (validation.Data.IsValid!=false)
            {
                string userInfoRespons = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                userInfoResponse = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoRespons);
            }
            else
            {
                throw new BusinessException("Invalid external authentication.");
            }
            return userInfoResponse;

        }
    }
}
