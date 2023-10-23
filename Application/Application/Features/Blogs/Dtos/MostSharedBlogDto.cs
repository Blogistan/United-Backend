namespace Application.Features.Blogs.Dtos
{
    public class MostSharedBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string BannerImageUrl { get; set; }
        public string WriterName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReadCount { get; set; }
    }
}
