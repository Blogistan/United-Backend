﻿using System.Text.Json.Serialization;

namespace Infrastructure.Dtos.Facebook
{
    public class FacebookUserInfoResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
