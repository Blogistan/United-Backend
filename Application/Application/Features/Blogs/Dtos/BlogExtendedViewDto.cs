using Application.Features.Categories.Dtos;
using Application.Features.Contents.Dtos;

namespace Application.Features.Blogs.Dtos
{
    public class BlogExtendedViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public CategoryViewDto Category { get; set; }
        public string BannerImageUrl { get; set; } = string.Empty;
        public int WriterId { get; set; }
        public string WriterName { get; set; }
        public ContentListViewDto Content { get; set; }
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
