namespace Domain.Entities
{
    public class Ban
    {
        public Guid UserBanID { get; set; }
        public Guid ReportID { get; set; }

        public virtual UserBan UserBan { get; set; }
        public virtual Report Report { get; set; }

        public Ban()
        {

        }
        public Ban(Guid userbanID, Guid reportID)
        {
            this.ReportID = reportID;
            this.UserBanID = userbanID;
        }
    }
}
