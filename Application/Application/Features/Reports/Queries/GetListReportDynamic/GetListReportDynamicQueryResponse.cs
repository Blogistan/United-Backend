using Application.Features.Reports.Dtos;
using Core.Application.Responses;

namespace Application.Features.Reports.Queries.GetListReportDynamic
{
    public record GetListReportDynamicQueryResponse :IResponse
    {
        public IList<ReportListViewDto> Items { get; set; }

        public GetListReportDynamicQueryResponse(IList<ReportListViewDto> items)
        {
            Items = items;
        }
    }
}
