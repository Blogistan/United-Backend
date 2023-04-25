using Core.Security.Entities;

namespace Domain.Entities
{
    public class Bookmark
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual User User { get; set; }


        public Bookmark()
        {

        }
        public Bookmark(int userId, int blogId)
        {
            this.BlogId = blogId;
            this.UserId = userId;
        }
    }
}
