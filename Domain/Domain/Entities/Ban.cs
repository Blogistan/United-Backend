using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Ban : Entity<Guid>
    {
        public Guid ReportID { get; set; }
        public int? UserID { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; }

        public virtual Report Report { get; set; }
        public virtual SiteUser User { get; set; }

        public Ban()
        {

        }
        public Ban(Guid id, int userID, Guid repotId, bool isPerma, DateTime banStartDate, DateTime BanEndDate, string? bandDetail) : this()
        {
            this.Id = id;
            this.UserID = userID;
            this.ReportID = repotId;
            this.IsPerma = isPerma;
            this.BanStartDate = banStartDate;
            this.BanEndDate = BanEndDate;
            this.BanDetail = bandDetail;
        }
    }
}
