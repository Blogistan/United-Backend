using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Report : Entity<Guid>
    {
        public int ReportTypeID { get; set; }
        public string ReportDescription { get; set; } = string.Empty;


        public virtual ICollection<SiteUser> Users { get; set; }
        public virtual ReportType ReportType { get; set; }
        public virtual UserBan UserBan { get; set; }


        public Report()
        {
            
        }
        public Report(Guid id,int userID,int reprotTypeID):this()
        {
            this.Id = id;
            this.ReportTypeID = reprotTypeID;
        }
    }
}
