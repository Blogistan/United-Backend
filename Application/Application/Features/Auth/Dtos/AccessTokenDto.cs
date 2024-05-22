namespace Application.Features.Auth.Dtos
{
    public class AccessTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
