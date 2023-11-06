namespace Application.Features.ReportTypes.Commands.UpdateReportType
{
    public class UpdateReportTypeCommandResponse
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;
    }
}
