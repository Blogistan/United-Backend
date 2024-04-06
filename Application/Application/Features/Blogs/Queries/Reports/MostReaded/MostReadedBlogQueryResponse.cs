using Application.Features.Blogs.Dtos;

namespace Application.Features.Blogs.Queries.Reports.MostReaded
{
    public class MostReadedBlogQueryResponse
    {
        public List<MostReadedBlogDto>  Items  { get; set; }

        public MostReadedBlogQueryResponse(List<MostReadedBlogDto> items)
        {
            Items = items;
        }
    }
}
