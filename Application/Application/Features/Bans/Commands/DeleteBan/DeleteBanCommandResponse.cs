using Application.Features.SiteUsers.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bans.Commands.DeleteBan
{
    public record DeleteBanCommandResponse : IResponse
    {
        public int Id { get; set; }
        public SiteUserListViewDto User { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

        public DeleteBanCommandResponse(int id, SiteUserListViewDto userName, bool isPerma, DateTime banStartDate, DateTime banEndDate, string? banDetail)
        {
            this.Id = id;
            this.User = userName;
            this.IsPerma = isPerma;
            this.BanStartDate = banStartDate;
            this.BanEndDate = banEndDate;
            this.BanDetail = banDetail;
        }

        public DeleteBanCommandResponse()
        {
        }
    }
}
