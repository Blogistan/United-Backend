

using Core.Security.Entities;
using Core.Security.Enums;

namespace Domain.Entities
{
    public class SiteUser : User
    {

        public string? ProfileImageUrl { get; set; } = String.Empty;
        public string? Biography { get; set; } = String.Empty;

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public SiteUser()
        {
            Bookmarks = new HashSet<Bookmark>();
        }
        public SiteUser(int id, string firstName, string lastName, string email, string profileImageUrl, string biography, byte[] passwordSalt, byte[] passwordHash,
                   bool status, AuthenticatorType authenticatorType, ICollection<Blog> blogs, ICollection<Bookmark> bookmarks) : this()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
            AuthenticatorType = authenticatorType;
            Bookmarks = bookmarks;
            Blogs = blogs;
            Biography = biography;
            ProfileImageUrl = profileImageUrl;
        }
    }
}
