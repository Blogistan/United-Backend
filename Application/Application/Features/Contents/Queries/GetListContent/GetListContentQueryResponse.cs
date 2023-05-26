using Application.Features.Contents.Dtos;

namespace Application.Features.Contents.Queries.GetListContent
{
    public class GetListContentQueryResponse
    {
        public List<ContentListViewDto> Items { get; set; }
    }
}
