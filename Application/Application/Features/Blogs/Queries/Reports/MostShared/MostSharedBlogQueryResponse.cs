using Application.Features.Blogs.Dtos;

namespace Application.Features.Blogs.Queries.Reports.MostShared
{
    public class MostSharedBlogQueryResponse
    {
        public MostSharedBlogDto Items { get; set; }

        public MostSharedBlogQueryResponse(MostSharedBlogDto items)
        {
            Items = items;
        }
    }
}
