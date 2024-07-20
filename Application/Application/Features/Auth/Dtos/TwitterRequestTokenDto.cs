namespace Application.Features.Auth.Dtos
{
    public class TwitterRequestTokenDto
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public bool oauth_callback_confirmed { get; set; }
    }
}
