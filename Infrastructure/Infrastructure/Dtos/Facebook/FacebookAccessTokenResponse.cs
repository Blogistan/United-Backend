using System.Text.Json.Serialization;

namespace Infrastructure.Dtos.Facebook
{
    public class FacebookAccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;
    }
}
