namespace Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery
{
    public class IncreaseDislikeOfCommentQueryResponse
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string? UserName { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
