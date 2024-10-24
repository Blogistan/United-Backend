using Core.Application.Responses;

namespace Application.Features.ReportTypes.Commands.UpdateReportType
{
    public record UpdateReportTypeCommandResponse :IResponse
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;

        public UpdateReportTypeCommandResponse(int id, string reportTypeName, string reportTypeDescription)
        {
            Id = id;
            ReportTypeName = reportTypeName;
            ReportTypeDescription = reportTypeDescription;
        }
    }
}
