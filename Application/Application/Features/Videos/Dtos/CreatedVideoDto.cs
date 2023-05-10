namespace Application.Features.Videos.Dtos
{
    public class CreatedVideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount { get; set; }
    }
}
