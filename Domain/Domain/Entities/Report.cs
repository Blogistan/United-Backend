using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Report : Entity<Guid>
    {
        public int BlogID { get; set; }
        public int ReportTypeID { get; set; }
        public string ReportDescription { get; set; } = string.Empty;


        public virtual Blog Blog { get; set; }
        public virtual ReportType ReportType { get; set; }


        public Report()
        {
            
        }
        public Report(Guid id,int blogID,int reprotTypeID):this()
        {
            this.Id = id;
            this.BlogID = blogID;
            this.ReportTypeID = reprotTypeID;
        }
    }
}
