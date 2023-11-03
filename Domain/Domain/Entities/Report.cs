using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Report : Entity<Guid>
    {
        public int ReportTypeID { get; set; }
        public int UserID { get; set; }
        public string ReportDescription { get; set; } = string.Empty;
        public virtual ICollection<Ban> Bans { get; set; }
        public virtual SiteUser User { get; set; }
        public virtual ReportType ReportType { get; set; }


        public Report()
        {
            
        }
        public Report(Guid id,int userID,int reprotTypeID):this()
        {
            this.Id = id;
            this.UserID = userID;
            this.ReportTypeID = reprotTypeID;

        }
    }
}
