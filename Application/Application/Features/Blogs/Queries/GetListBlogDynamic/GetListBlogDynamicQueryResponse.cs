using Application.Features.Blogs.Dtos;

namespace Application.Features.Blogs.Queries.GetListBlogDynamic
{
    public class GetListBlogDynamicQueryResponse
    {
        public List<BlogListViewDto> Items { get; set; }
        public GetListBlogDynamicQueryResponse(List<BlogListViewDto> items)
        {
            Items = items;
        }
    }
}
