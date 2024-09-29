using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Comment : Entity<int>
    {
        public int? SiteUserId { get; set; }

        public string? GuestName { get; set; } = String.Empty;

        public string CommentContent { get; set; } = String.Empty;

        public int Likes { get; set; }

        public int Dislikes { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }

        public virtual ICollection<Comment>? CommentResponses { get; set; }

        public virtual SiteUser SiteUser { get; set; }



        public Comment()
        {

        }

        public Comment(int id, int userId, string guestName, string commentContent, int likes, int disLikes, ICollection<Comment> commentsResponsers)
        {
            this.Id = id;
            this.Likes = likes;
            this.Dislikes = disLikes;
            this.GuestName = guestName;
            this.CommentContent = commentContent;
            this.CommentResponses = commentsResponsers;
            this.SiteUserId = userId;
        }
    }
}
