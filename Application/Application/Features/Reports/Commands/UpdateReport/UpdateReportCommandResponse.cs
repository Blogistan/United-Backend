using Core.Application.Responses;

namespace Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandResponse:IResponse
    {
        public int Id { get; set; }
        public string ReportDescription { get; set; } = string.Empty;


        public UpdateReportCommandResponse()
        {
            
        }
        public UpdateReportCommandResponse(int id, string reportDescription)
        {
            Id = id;
            ReportDescription = reportDescription;
        }
    }
}
