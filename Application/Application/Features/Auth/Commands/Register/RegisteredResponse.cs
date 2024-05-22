using Application.Features.Auth.Dtos;
using Core.Application.Responses;
using Core.Security.Entities;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisteredResponse:IResponse
    {
        public AccessTokenDto AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
