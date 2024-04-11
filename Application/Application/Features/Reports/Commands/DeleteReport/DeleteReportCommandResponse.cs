using Core.Application.Responses;

namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandResponse:IResponse
    {
        public Guid Id { get; set; }
        public int ReportType { get; set; } 
        public string ReportDescription { get; set; } = string.Empty;

        public DeleteReportCommandResponse()
        {
            
        }
        public DeleteReportCommandResponse(Guid id, int reportType, string reportDescription)
        {
            Id = id;
            ReportType = reportType;
            ReportDescription = reportDescription;
        }
    }
}
