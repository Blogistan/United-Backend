using Application.Features.ReportTypes.Dtos;

namespace Application.Features.ReportTypes.Queries.GetListReportTypes
{
    public class GetListReportTypeQueryResponse
    {
        public List<ReportTypeListViewDto> Items { get; set; }
    }
}
