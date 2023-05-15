using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Comment : Entity<int>
    {
        public int? UserId { get; set; }

        public string? GuestName { get; set; }

        public string CommentContent { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
        public int? BlogId { get; set; }
        public int? ParentCommentId { get; set; }

        public virtual ICollection<Comment> CommentResponses { get; set; }

        public virtual User User { get; set; }



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
            this.UserId = userId;
        }
    }
}
