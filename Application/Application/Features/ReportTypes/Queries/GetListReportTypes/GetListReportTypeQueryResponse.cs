using Application.Features.ReportTypes.Dtos;
using Core.Application.Responses;

namespace Application.Features.ReportTypes.Queries.GetListReportTypes
{
    public record GetListReportTypeQueryResponse :IResponse
    {
        public List<ReportTypeListViewDto> Items { get; set; }

        public GetListReportTypeQueryResponse(List<ReportTypeListViewDto> items)
        {
            Items = items;
        }
    }
}
