using Core.Application.Responses;

namespace Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery
{
    public class IncreaseLikeOfCommentQueryResponse : IResponse
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string? UserName { get; set; }
        public string? GuestName { get; set; }
        public string CommentContent { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }

        public IncreaseLikeOfCommentQueryResponse(int id, int? blogId, string? userName, string? guestName, string commentContent, int likes, int dislikes, int? parentCommentId)
        {
            this.Id = id;
            BlogId = blogId;
            UserName = userName;
            GuestName = guestName;
            CommentContent = commentContent;
            Likes = likes;
            Dislikes = dislikes;
            ParentCommentId = parentCommentId;
        }

    }
}
