using Application.Features.Blogs.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bookmarks.Queries.GetListBookmarks
{
    public class GetListBookmarkQueryResponse:IResponse
    {
        public List<BlogListViewDto> Items { get; set; }

        public GetListBookmarkQueryResponse(List<BlogListViewDto> items)
        {
            Items = items;
        }
    }
}
