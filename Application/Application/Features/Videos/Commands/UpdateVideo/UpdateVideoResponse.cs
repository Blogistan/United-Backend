using Core.Application.Responses;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoResponse:IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public int WatchCount { get; set; }

        public UpdateVideoResponse()
        {
            
        }
        public UpdateVideoResponse(int id, string title, string videoUrl, string description, int watchCount)
        {
            Id = id;
            Title = title;
            VideoUrl = videoUrl;
            Description = description;
            WatchCount = watchCount;
        }
    }
}
