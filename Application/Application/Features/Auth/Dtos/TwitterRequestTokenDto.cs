namespace Application.Features.Auth.Dtos
{
    public record TwitterRequestTokenDto
    {
        public string oauth_token { get; set; } = string.Empty;
        public string oauth_token_secret { get; set; } = string.Empty;
        public bool oauth_callback_confirmed { get; set; }
    }
}
