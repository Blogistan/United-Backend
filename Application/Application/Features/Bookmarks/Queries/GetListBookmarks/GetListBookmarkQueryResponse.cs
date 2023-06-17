using Application.Features.Blogs.Dtos;

namespace Application.Features.Bookmarks.Queries.GetListBookmarks
{
    public class GetListBookmarkQueryResponse
    {
        public List<BlogListViewDto> Items { get; set; }
    }
}
