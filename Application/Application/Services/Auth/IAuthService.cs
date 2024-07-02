using Core.Security.Entities;
using Core.Security.JWT;
using Infrastructure;

namespace Application.Services.Auth
{
    public interface IAuthService: IExternalAuthService
    {
        Task<AccessToken> CreateAccessToken(UserBase user);

        Task<RefreshToken> CreateRefreshToken(UserBase user, string IpAddress);

        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);

        Task DeleteOldActiveRefreshTokens(UserBase user);

        Task RevokeDescendantRefreshTokens(RefreshToken token, string IpAddress, string reason);
        public Task RevokeRefreshToken(RefreshToken refreshToken, string IpAddress, string reason, string? replacedByToken);
        Task<RefreshToken> RotateRefreshToken(UserBase user, RefreshToken refreshToken, string ipAddress);

        Task<EmailAuthenticator> CreateEmailAutenticator(UserBase user);

        public Task<OtpAuthenticator> CreateOtpAuthenticator(UserBase user);
        Task<string> ConvertOtpKeyToString(byte[] secretBtyes);

        Task SendAuthenticatorCode(UserBase user);
        Task VerifyAuthenticatorCode(UserBase user, string code);
    }
}
