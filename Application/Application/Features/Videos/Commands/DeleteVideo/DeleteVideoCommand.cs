using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommand : IRequest<DeleteVideoResponse>
    {
        public int Id { get; set; }

        public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly VideoBusinessRules videoBusinessRules;
            public DeleteVideoCommandHandler(IVideoRepository videoRepository, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
            {
                var video = await videoBusinessRules.VideoCheckById(request.Id);

                Video deletedVideo = await videoRepository.DeleteAsync(video);

                return new DeleteVideoResponse
                {
                    Id = deletedVideo.Id,
                    Title = deletedVideo.Title,
                    WatchCount = deletedVideo.WatchCount,
                    Description = deletedVideo.Description,
                    VideoUrl = deletedVideo.VideoUrl
                };
            }
        }
    }
}
