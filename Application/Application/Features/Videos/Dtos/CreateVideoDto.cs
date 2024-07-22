namespace Application.Features.Videos.Dtos
{
    public class CreateVideoDto
    {
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int WatchCount => 0;
    }
}
