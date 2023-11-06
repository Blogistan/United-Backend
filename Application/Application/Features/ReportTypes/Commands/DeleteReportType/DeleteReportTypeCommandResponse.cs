namespace Application.Features.ReportTypes.Commands.DeleteReportType
{
    public class DeleteReportTypeCommandResponse
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;
    }
}
