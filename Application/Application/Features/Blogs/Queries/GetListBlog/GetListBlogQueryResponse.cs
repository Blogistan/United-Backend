using Application.Features.Blogs.Dtos;
using Core.Application.Responses;

namespace Application.Features.Blogs.Queries.GetListBlog
{
    public class GetListBlogQueryResponse:IResponse
    {
        public List<BlogListViewDto> Items { get; set; }

        public GetListBlogQueryResponse(List<BlogListViewDto> items)
        {
            Items = items;
        }

    }
}
