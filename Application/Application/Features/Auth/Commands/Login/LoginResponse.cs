using Core.Application.Responses;
using Core.Security.Enums;
using Core.Security.JWT;
using Infrastructure.Dtos;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginResponse:LoginResponseBase,IResponse
    {
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }

        public class LoggedHttpResponse
        {
            public AccessToken? AccessToken { get; set; }
            public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        }
        public LoggedHttpResponse ToHttpResponse() => new()
        {
            AccessToken = AccessToken,
            RequiredAuthenticatorType = RequiredAuthenticatorType
        };


    }
}
