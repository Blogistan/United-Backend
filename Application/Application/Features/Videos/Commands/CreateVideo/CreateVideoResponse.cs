namespace Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount { get; set; }
    }
}
