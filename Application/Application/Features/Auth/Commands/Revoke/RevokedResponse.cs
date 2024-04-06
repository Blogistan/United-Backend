using Core.Application.Responses;

namespace Application.Features.Auth.Commands.Revoke
{
    public class RevokedResponse:IResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
