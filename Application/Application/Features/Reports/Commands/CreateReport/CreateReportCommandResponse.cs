using Core.Application.Responses;

namespace Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandResponse:IResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;

        public CreateReportCommandResponse()
        {
            
        }
        public CreateReportCommandResponse(Guid id, string userName, string reportType, string reportDescription)
        {
            Id = id;
            UserName = userName;
            ReportType = reportType;
            ReportDescription = reportDescription;
        }
    }
}
