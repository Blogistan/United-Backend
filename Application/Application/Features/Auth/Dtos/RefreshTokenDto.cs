namespace Application.Features.Auth.Dtos
{
    public class RefreshTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        public string? ReasonRevoked { get; set; }
    }
}
