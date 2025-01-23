namespace Application.Features.Auth.Commands.ExternalLoginUrl.Google
{
    public record GetGoogleClientCommandResponse
    {
        public string Client { get; set; }
    }
}
