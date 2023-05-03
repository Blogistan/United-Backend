using Core.Security.Entities;

namespace Domain.Entities
{
    public class Bookmark
    {
        public int SiteUserId { get; set; }
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual SiteUser SiteUser { get; set; }


        public Bookmark()
        {

        }
        public Bookmark(int userId, int blogId)
        {
            this.BlogId = blogId;
            this.SiteUserId = userId;
        }
    }
}
