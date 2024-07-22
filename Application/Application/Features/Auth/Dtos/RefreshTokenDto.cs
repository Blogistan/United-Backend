namespace Application.Features.Auth.Dtos
{
    public class RefreshTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; } = string.Empty;
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; } = string.Empty;
        public string? ReplacedByToken { get; set; } = string.Empty;

        public string? ReasonRevoked { get; set; } = string.Empty;
    }
}
