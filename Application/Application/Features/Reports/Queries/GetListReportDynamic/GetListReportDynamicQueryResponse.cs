using Application.Features.Reports.Dtos;

namespace Application.Features.Reports.Queries.GetListReportDynamic
{
    public class GetListReportDynamicQueryResponse
    {
        public IList<ReportListViewDto> Items { get; set; }
    }
}
