using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Content : Entity<int>
    {
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }


        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<VideoContent> VideoContents { get; set; }

        public Content()
        {

        }

        public Content(int id, string title, string contentImageUrl, string contentPragraph)
        {

            this.Id = id;
            this.Title = title;
            this.ContentPragraph = contentPragraph;
            this.ContentPragraph = contentPragraph;
        }
    }
}
