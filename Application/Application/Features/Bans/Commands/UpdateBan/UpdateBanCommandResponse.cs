using Core.Application.Responses;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public class UpdateBanCommandResponse:IResponse
    {
        public int Id { get; set; }
        public int ReportID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

        public UpdateBanCommandResponse(int id, string userName, bool isPerma, DateTime banStartDate, DateTime banEndDate, string? banDetail)
        {
            this.Id = id;
            this.UserName = userName;
            this.IsPerma = isPerma;
            this.BanStartDate = banStartDate;
            this.BanEndDate = banEndDate;
            this.BanDetail = banDetail;
        }
    }
}
