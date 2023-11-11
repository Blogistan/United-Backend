﻿namespace Application.Features.Bans.Commands.DeleteBan
{
    public class DeleteBanCommandResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
    }
}
