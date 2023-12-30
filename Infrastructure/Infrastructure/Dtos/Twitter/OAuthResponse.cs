namespace Infrastructure.Dtos.Twitter
{
    public class OAuthResponse
    {
        public string Oauth_token { get; set; } = string.Empty;
        public string Oauth_token_secret { get; set; } = string.Empty;
        public Dictionary<string, string> Cookies { get; set; }
    }
}
