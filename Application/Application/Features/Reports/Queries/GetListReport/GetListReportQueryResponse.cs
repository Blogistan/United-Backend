using Application.Features.Reports.Dtos;
using Core.Application.Responses;

namespace Application.Features.Reports.Queries.GetListReport
{
    public class GetListReportQueryResponse:IResponse
    {
        public IList<ReportListViewDto> Items { get; set; }
        public GetListReportQueryResponse(IList<ReportListViewDto> items)
        {
            Items = items;
        }
    }
}
