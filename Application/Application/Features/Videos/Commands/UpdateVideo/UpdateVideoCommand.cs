using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommand : IRequest<UpdateVideoResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount { get; set; }

        public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly VideoBusinessRules videoBusinessRules;
            public UpdateVideoCommandHandler(IVideoRepository videoRepository, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
            {
                await videoBusinessRules.VideoCannotBeDuplicatedWhenUpdated(request.Title, request.VideoUrl);

                Video video = new()
                {
                    Id = request.Id,
                    Title = request.Title,
                    VideoUrl = request.VideoUrl,
                    Description = request.Description
                };

                Video updatedVideo = await videoRepository.UpdateAsync(video);

                return new UpdateVideoResponse
                {
                    Id = updatedVideo.Id,
                    Description = updatedVideo.Description,
                    Title = updatedVideo.Title,
                    VideoUrl = updatedVideo.VideoUrl,
                    WatchCount = updatedVideo.WatchCount
                };

            }
        }
    }
}
