namespace Application.Features.Blogs.Dtos
{
    public class MostReadedBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
        public string WriterName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int ReadCount { get; set; }
    }
}
