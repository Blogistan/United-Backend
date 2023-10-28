using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class ReportType:Entity<int>
    {
        public string ReportTypeName { get; set; }
        public string ReportTypeDescription { get; set; }
    }
}
