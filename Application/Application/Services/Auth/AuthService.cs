using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Persistence.Paging;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Text;

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

        public AuthService(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, ISiteUserRepository siteUserRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IUserOperationClaimRepository userOperationClaimRepository, IMailService mailService,
            IOtpAuthenticatorHelper otpAuthenticatorHelper,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            IOtpAuthenticatorRepository otpAuthenticatorRepository)
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
        
    }
}
