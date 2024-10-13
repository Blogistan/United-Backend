﻿namespace Application.Features.Auth.Dtos
{
    public record AccessTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
