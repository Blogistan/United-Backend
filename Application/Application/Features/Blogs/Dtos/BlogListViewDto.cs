namespace Application.Features.Blogs.Dtos
{
    public record BlogListViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
        public int WriterId { get; set; }
        public string WriterName { get; set; } = string.Empty;
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
