using Domain.Entities;
using Infrastructure.Dtos;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Infrastructure
{
    public interface IExternalAuthService
    {
        Task<GoogleLoginResponse> CreateUserExternalAsync(SiteUser user, string email, string name, string surname, string picture, string ipAdress);
        Task<Payload> GoogleSignIn(string idToken);
    }
}
