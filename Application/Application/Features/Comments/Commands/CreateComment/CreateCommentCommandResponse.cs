using Application.Features.Comments.Dtos;
using Core.Application.Responses;

namespace Application.Features.Comments.Commands.CreateComment
{
    public record CreateCommentCommandResponse :IResponse
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
        public string? UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProfileImageUrl { get; set; }
        public string? GuestName { get; set; }
        public string CommentContent { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<CommentViewDto> CommentResponses { get; set; }

        public CreateCommentCommandResponse()
        {
            
        }


    }
}
