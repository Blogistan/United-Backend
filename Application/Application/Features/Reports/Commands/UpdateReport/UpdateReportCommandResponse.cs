namespace Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandResponse
    {
        public Guid Id { get; set; }
        public string ReportDescription { get; set; } = string.Empty;
    }
}
