using Application.Features.Comments.Dtos;
using Core.Application.Responses;

namespace Application.Features.Comments.Queries.GetBlogCommentsQuery
{
    public class GetBlogCommentsQueryResponse:IResponse
    {
        public CommentViewDto commentViewDto { get; set; }

        public GetBlogCommentsQueryResponse()
        {
            
        }
        public GetBlogCommentsQueryResponse(CommentViewDto commentViewDto)
        {
            this.commentViewDto = commentViewDto;
        }
    }
}
