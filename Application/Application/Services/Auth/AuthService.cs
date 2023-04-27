using Application.Services.Repositories;
using Core.Mailing;
using Core.Persistence.Paging;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Microsoft.EntityFrameworkCore;

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

        public AuthService(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, ISiteUserRepository siteUserRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IUserOperationClaimRepository userOperationClaimRepository, IMailService mailService,
            IOtpAuthenticatorHelper otpAuthenticatorHelper,
            IEmailAuthenticatorHelper emailAuthenticatorHelper)
        {
            this.tokenHelper = tokenHelper;
            this.mailService = mailService;
            this.refreshTokenRepository = refreshTokenRepository;
            this.siteUserRepository = siteUserRepository;
            this.emailAuthenticatorRepository = emailAuthenticatorRepository;
            this.userOperationClaimRepository = userOperationClaimRepository;
            this.otpAuthenticatorHelper = otpAuthenticatorHelper;
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
            ICollection<RefreshToken> oldActiveTokens = await refreshTokenRepository.GetAllOldActiveRefreshTokenAsync(user,tokenHelper.RefreshTokenTTLOption);

            await refreshTokenRepository.DeleteAsync(oldActiveTokens.ToList());
        }

        public async Task RevokeDescendantRefreshTokens(RefreshToken token, string IpAddress, string reason)
        {
            
        }

        public Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task SendAuthenticatorCode(User user)
        {
            throw new NotImplementedException();
        }

        public Task VerifyAuthenticatorCode(User user, string code)
        {
            throw new NotImplementedException();
        }
    }
}
