namespace Application.Features.Auth.Commands.ExternalLoginUrl.Github
{
    public record GetGithubLoginLinkCommandResponse
    {
        public string Url { get; set; }
    }
}
