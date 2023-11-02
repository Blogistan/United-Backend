namespace Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandResponse
    {
        public Guid Id { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
    }
}
