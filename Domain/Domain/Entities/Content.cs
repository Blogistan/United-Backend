using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Content : Entity<int>
    {
        public string Title { get; set; } = string.Empty;

        public string? ContentImageUrl { get; set; } = string.Empty;

        public string ContentPragraph { get; set; } = string.Empty;

        public virtual ICollection<Blog> Blogs { get; set; }

        //public virtual ICollection<Video>? Videos { get; set; }
        //public virtual ICollection<VideoContent>? VideoContents { get; set; }

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
