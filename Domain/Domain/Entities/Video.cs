using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Video : Entity<int>
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }

        public Video()
        {

        }
        public Video(int id, string title, string videoUrl, string description, int watchCount = 0)
        {
            this.Id = id;
            this.Title = title;
            this.VideoUrl = videoUrl;
            this.Description = description;
            this.WatchCount = watchCount;
        }
    }
}
