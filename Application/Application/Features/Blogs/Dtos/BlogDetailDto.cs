using Application.Features.Categories.Dtos;

namespace Application.Features.Blogs.Dtos
{
    public class BlogDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CategoryViewDto Category { get; set; }
        public string WriterName { get; set; }
        public string TitleContent { get; set; }
        public string? ContentImageUrl { get; set; }
        public string ContentPragraph { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }

        public int ShareCount { get; set; }
        public int ReadCount { get; set; }
    }
}
