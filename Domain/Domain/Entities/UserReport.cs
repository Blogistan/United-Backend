namespace Domain.Entities
{
    public class UserReport
    {
        public int UserID { get; set; }
        public Guid ReportID { get; set; }


        public virtual SiteUser User { get; set; }
        public virtual Report Report { get; set; }

        public UserReport()
        {

        }
        public UserReport(int userID, Guid reportID)
        {
            this.ReportID = reportID;
            this.UserID = userID;
        }

    }
}
