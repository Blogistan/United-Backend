using Application.Features.Reports.Dtos;

namespace Application.Features.Reports.Queries.GetListReport
{
    public class GetListReportQueryResponse
    {
        public IList<ReportListViewDto> Items { get; set; }
    }
}
