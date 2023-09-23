using Core.Security.Entities;
using Core.Security.JWT;

namespace Infrastructure.Dtos
{
    public class GoogleLoginResponse
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
