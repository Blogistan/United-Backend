using Core.Security.JWT;

namespace Infrastructure.ExternalLogin
{
    public interface IExternalAuthenticationService
    {
        Task<AccessToken> GoogleLoginAsync(string idToken, int tokenLifeTime);
    }
}
