using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Report : Entity<Guid>
    {
        public int ReportTypeId { get; set; }
        public int SiteUserId { get; set; }
        public string ReportDescription { get; set; } = string.Empty;
        public virtual ICollection<Ban> Bans { get; set; }
        public virtual SiteUser SiteUser { get; set; }
        public virtual ReportType ReportType { get; set; }


        public Report()
        {
            
        }
        public Report(Guid id,int userID,int reprotTypeID):this()
        {
            this.Id = id;
            this.SiteUserId = userID;
            this.ReportTypeId = reprotTypeID;

        }
    }
}
