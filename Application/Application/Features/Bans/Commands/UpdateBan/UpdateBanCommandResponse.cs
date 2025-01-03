﻿using Application.Features.SiteUsers.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public record UpdateBanCommandResponse :IResponse
    {
        public int Id { get; set; }
        public SiteUserListViewDto User { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

        public UpdateBanCommandResponse(int id, SiteUserListViewDto userName, bool isPerma, DateTime banStartDate, DateTime banEndDate, string? banDetail)
        {
            this.Id = id;
            this.User = userName;
            this.IsPerma = isPerma;
            this.BanStartDate = banStartDate;
            this.BanEndDate = banEndDate;
            this.BanDetail = banDetail;
        }

        public UpdateBanCommandResponse()
        {
        }
    }
}
