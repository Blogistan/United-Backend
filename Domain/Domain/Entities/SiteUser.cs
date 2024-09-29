using Core.Persistence.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;

namespace Domain.Entities
{
    public class SiteUser :Entity<int>
    {
        public int UserId { get; set; }
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string? Biography { get; set; } = string.Empty;
        public bool? IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Ban> Bans { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public SiteUser()
        {
            Bookmarks = new HashSet<Bookmark>();
        }
        public SiteUser(int id,int userId, string profileImageUrl, string biography,bool isVerified,ICollection<Blog> blogs, ICollection<Bookmark> bookmarks) : this()
        {
            Id = id;
            UserId = userId;
            IsVerified = isVerified;
            Bookmarks = bookmarks;
            Blogs = blogs;
            Biography = biography;
            ProfileImageUrl = profileImageUrl;
        }
    }
}
