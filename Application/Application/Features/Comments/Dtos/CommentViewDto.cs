namespace Application.Features.Comments.Dtos
{
    public class CommentViewDto
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string? UserName { get; set; }
        public string? GuestName { get; set; }
        public string CommentContent { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<CommentViewDto> CommentResponses { get; set; }
    }
}
