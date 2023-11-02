namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandResponse
    {
        public Guid Id { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
    }
}
