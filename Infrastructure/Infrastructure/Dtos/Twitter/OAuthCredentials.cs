namespace Infrastructure.Dtos.Twitter
{
    public class OAuthCredentials
    {
        public string Oauth_token { get; set; } = string.Empty;
        public string Oauth_verifier { get; set; } = string.Empty;
    }
}
