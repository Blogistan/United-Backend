using Core.Application.Responses;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentResponse:IResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }
        public int? BlogId { get; set; }

        public UpdateCommentResponse(int id, string? userName, string? guestName, string commentContent, int likes, int dislikes, int? parentCommentId, int? blogId)
        {
            Id = id;
            UserName = userName;
            GuestName = guestName;
            CommentContent = commentContent;
            Likes = likes;
            Dislikes = dislikes;
            ParentCommentId = parentCommentId;
            BlogId = blogId;
        }

    }
}
