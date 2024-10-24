using Core.Application.Responses;

namespace Application.Features.Reports.Commands.CreateReport
{
    public record CreateReportCommandResponse :IResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;

        public CreateReportCommandResponse()
        {
            
        }
        public CreateReportCommandResponse(int id, string userName, string reportType, string reportDescription)
        {
            Id = id;
            UserName = userName;
            ReportType = reportType;
            ReportDescription = reportDescription;
        }
    }
}
