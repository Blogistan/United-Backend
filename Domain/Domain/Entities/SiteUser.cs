using Core.Security.Entities;
using Core.Security.Enums;

namespace Domain.Entities
{
    public class SiteUser : User
    {

        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string? Biography { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime VerifiedAt { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<UserBan> UserBans { get; set; }
        public virtual ICollection<UserReport> UserReports { get; set; }

        public SiteUser()
        {
            Bookmarks = new HashSet<Bookmark>();
        }
        public SiteUser(int id, string firstName, string lastName, string email, string profileImageUrl, string biography, byte[] passwordSalt, byte[] passwordHash,
                   bool isActive, AuthenticatorType authenticatorType, ICollection<Blog> blogs, ICollection<Bookmark> bookmarks) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            IsActive = isActive;
            AuthenticatorType = authenticatorType;
            Bookmarks = bookmarks;
            Blogs = blogs;
            Biography = biography;
            ProfileImageUrl = profileImageUrl;
        }
    }
}
