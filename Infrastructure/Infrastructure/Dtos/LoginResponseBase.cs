using Core.Security.Entities;
using Core.Security.JWT;

namespace Infrastructure.Dtos
{
    public abstract class LoginResponseBase
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
