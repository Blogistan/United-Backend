using Core.Security.Entities;

namespace Domain.Entities
{
    public class UserReport
    {
        public int UserId { get; set; }
        public Guid ReportId { get; set; }
        public virtual SiteUser SiteUser { get; set; }
        public virtual Report Report { get; set; }

        public UserReport()
        {

        }
        public UserReport(int userID, Guid reportID)
        {
            this.ReportId = reportID;
            this.UserId = userID;
        }

    }
}
