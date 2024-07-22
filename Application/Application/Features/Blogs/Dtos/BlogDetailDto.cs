using Application.Features.Categories.Dtos;

namespace Application.Features.Blogs.Dtos
{
    public class BlogDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public CategoryViewDto Category { get; set; }
        public string WriterName { get; set; } = string.Empty;
        public string TitleContent { get; set; } = string.Empty;
        public string? ContentImageUrl { get; set; }
        public string ContentPragraph { get; set; } = string.Empty;
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
