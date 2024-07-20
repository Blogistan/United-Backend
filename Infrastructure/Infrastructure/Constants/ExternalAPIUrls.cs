namespace Infrastructure.Constants
{
    public static class ExternalAPIUrls
    {
        public const string TwitterAccessToken = "https://api.twitter.com/oauth/access_token";
        public const string TwitterUserInfo = "https://api.twitter.com/1.1/account/verify_credentials.json?include_email=true";
        public const string TwitterRequestToken = "https://api.twitter.com/oauth/request_token";

        public const string GithubAccessToken = "https://github.com/login/oauth/access_token";
        public const string GithubUserInfo = "https://api.github.com/user";
    }
}
