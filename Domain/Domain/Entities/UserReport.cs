namespace Domain.Entities
{
    public class UserReport
    {
        public int SiteUserID { get; set; }
        public Guid ReportID { get; set; }


        public virtual SiteUser SiteUser { get; set; }
        public virtual Report Report { get; set; }

        public UserReport()
        {

        }
        public UserReport(int userID, Guid reportID)
        {
            this.ReportID = reportID;
            this.SiteUserID = userID;
        }

    }
}
