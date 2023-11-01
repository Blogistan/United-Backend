using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Report : Entity<Guid>
    {
        public int ReportTypeID { get; set; }
        public string ReportDescription { get; set; } = string.Empty;
        public virtual ICollection<Ban> Bans { get; set; }
        public virtual ICollection<UserReport> UserReports { get; set; }
        public virtual ReportType ReportType { get; set; }


        public Report()
        {
            
        }
        public Report(Guid id,int reprotTypeID):this()
        {
            this.Id = id;
            this.ReportTypeID = reprotTypeID;

        }
    }
}
