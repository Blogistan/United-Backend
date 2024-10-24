using Core.Application.Responses;

namespace Application.Features.Reports.Commands.DeleteReport
{
    public record DeleteReportCommandResponse :IResponse
    {
        public int Id { get; set; }
        public int ReportType { get; set; } 
        public string ReportDescription { get; set; } = string.Empty;

        public DeleteReportCommandResponse()
        {
            
        }
        public DeleteReportCommandResponse(int id, int reportType, string reportDescription)
        {
            Id = id;
            ReportType = reportType;
            ReportDescription = reportDescription;
        }
    }
}
