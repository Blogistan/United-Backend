namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandResponse
    {
        public Guid Id { get; set; }
        public int ReportType { get; set; } 
        public string ReportDescription { get; set; } = string.Empty;
    }
}
