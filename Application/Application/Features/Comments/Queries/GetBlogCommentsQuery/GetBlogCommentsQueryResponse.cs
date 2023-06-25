using Application.Features.Comments.Dtos;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQueryResponse
    {
        public List<CommentViewDto>  Items { get; set; }
    }
}
