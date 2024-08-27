using Core.Application.Responses;

namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandResponse : IResponse
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string? UserName { get; set; }
        public string? UserProfileImageUrl { get; set; }
        public string? GuestName { get; set; }
        public string CommentContent { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }

        public DeleteCommentCommandResponse()
        {

        }

        public DeleteCommentCommandResponse(int? blogId, string? userName, string? guestName, string commentContent, int likes, int dislikes, int? parentCommentId)
        {
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
