using Domain.Entities;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public class UpdateBanCommandResponse
    {
        public Guid Id { get; set; }
        public Guid ReportID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
    }
}
