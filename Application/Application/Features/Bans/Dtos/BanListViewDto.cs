using Application.Features.SiteUsers.Dtos;

namespace Application.Features.Bans.Dtos
{
    public record BanListViewDto
    {
        public int Id { get; set; }
        public SiteUserListViewDto User { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
    }
}
