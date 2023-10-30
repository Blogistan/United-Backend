using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class UserBan : Entity<Guid>
    {
        public Guid ReportID { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; }

        public virtual ICollection<SiteUser> SiteUser { get; set; }
        public virtual ICollection<Ban> Bans { get; set; }
        public virtual Report Report { get; set; }

        public UserBan()
        {

        }
        public UserBan(Guid id, int userID, Guid repotId, bool isPerma, DateTime banStartDate, DateTime BanEndDate, string? bandDetail) : this()
        {
            this.Id = id;
            this.ReportID = repotId;
            this.IsPerma = isPerma;
            this.BanStartDate = banStartDate;
            this.BanEndDate = BanEndDate;
            this.BanDetail = bandDetail;
        }
    }
}
