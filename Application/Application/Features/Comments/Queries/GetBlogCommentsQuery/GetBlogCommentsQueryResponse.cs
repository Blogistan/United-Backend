using Application.Features.Comments.Dtos;
using Core.Application.Responses;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQueryResponse:IResponse
    {
        public List<CommentViewDto> Items { get; set; }

        public GetBlogCommentsQueryResponse()
        {
            
        }
        public GetBlogCommentsQueryResponse(List<CommentViewDto> commentViewDto)
        {
            this.Items = commentViewDto;
        }
    }
}
