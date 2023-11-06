namespace Application.Features.ReportTypes.Commands.CreateReportType
{
    public class CreateReportTypeCommandResponse
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;
    }
}
