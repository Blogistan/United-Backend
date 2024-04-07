using Application.Features.Contents.Dtos;
using Core.Application.Responses;

namespace Application.Features.Contents.Queries.GetListContent
{
    public class GetListContentQueryResponse:IResponse
    {
        public List<ContentListViewDto> Items { get; set; }

        public GetListContentQueryResponse(List<ContentListViewDto> items)
        {
            Items = items;
        }
    }
}
