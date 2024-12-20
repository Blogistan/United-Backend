using Application.Features.Contents.Dtos;
using Core.Application.Responses;

namespace Application.Features.Contents.Queries.GetListContentDynamic
{
    public record GetListContentDynamicQueryResponse : IResponse
    {
        public List<ContentListViewDto> Items { get; set; }

        public GetListContentDynamicQueryResponse(List<ContentListViewDto> items)
        {
            Items = items;
        }
    }
}
