using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AccessToken> CreateAccessToken(User user);

        Task<RefreshToken> CreateRefreshToken(User user, string IpAddress);

        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);

        Task DeleteOldActiveRefreshTokens(User user);

        Task RevokeDescendantRefreshTokens(RefreshToken token, string IpAddress, string reason);
        public Task RevokeRefreshToken(RefreshToken refreshToken, string IpAddress, string reason, string? replacedByToken);
        Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress);

        Task<EmailAuthenticator> CreateEmailAutenticator(User user);

        public Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
        Task<string> ConvertOtpKeyToString(byte[] secretBtyes);

        Task SendAuthenticatorCode(User user);
        Task VerifyAuthenticatorCode(User user, string code);
    }
}
