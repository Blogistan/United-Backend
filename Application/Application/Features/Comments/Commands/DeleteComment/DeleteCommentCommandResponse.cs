namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandResponse
    {
        public int? BlogId { get; set; }
        public string? UserName { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
