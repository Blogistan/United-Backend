using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    int RefreshTokenTTLOption { get; }

    AccessToken CreateToken(UserBase user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(UserBase user, string ipAddress);
}
