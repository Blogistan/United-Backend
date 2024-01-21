namespace Infrastructure.Constants
{
    public static class ExternalAPIUrls
    {
        public const string TwitterAccessToken = "https://api.twitter.com/oauth/access_token";
        public const string UserInfo = "https://api.twitter.com/1.1/account/verify_credentials.json?include_email=true&include_entities=true";
    }
}
