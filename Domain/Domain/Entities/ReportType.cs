using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class ReportType:Entity<int>
    {
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;

        public virtual ICollection<Report> Reports { get; set; }


        public ReportType()
        {
            
        }
        public ReportType(int id,string reportTypeName,string reportTypeDescription):this()
        {
            this.Id = id;
            this.ReportTypeName = reportTypeName;
            this.ReportTypeDescription = reportTypeDescription;
        }
    }
}
