using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Blog : Entity<int>
    {
        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string BannerImageUrl { get; set; } = string.Empty;
        public int WriterId { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }

        public int ShareCount { get; set; }
        public int ReadCount { get; set; }
        public int ContentId { get; set; }
        public virtual SiteUser Writer { get; set; }

        public virtual Content Content { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Bookmark> FavoritedUsers { get; set; }
        public virtual Category Category { get; set; }

        public Blog()
        {
        }
        public Blog(int id, string title, int categoryId, string bannerImageUrl, int writerId,int contentId, ICollection<Comment> comments,
            int reactionSuprisedCount, int reactionLovelyCount, int reactionSadCount, int reactionTriggeredCount, int reactionKEKWCount, int shareCount, int readCount) : this()
        {
            this.Id = id;
            this.Title = title;
            this.CategoryId = categoryId;
            this.WriterId = writerId;
            this.BannerImageUrl = bannerImageUrl;
            this.ContentId = contentId;
            this.Comments = comments;
            this.ReadCount = readCount;
            this.ReactionKEKWCount = reactionKEKWCount;
            this.ReactionLovelyCount = reactionSadCount;
            this.ReactionSadCount = reactionSadCount;
            this.ReactionSuprisedCount = reactionSuprisedCount;
            this.ReactionTriggeredCount = reactionTriggeredCount;
            this.ShareCount = shareCount;
        }
    }
}
