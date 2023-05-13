using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Videos.Queries.GetById
{
    public class GetVideoByIdQuery : IRequest<VideoViewDto>
    {
        public int Id { get; set; }


        public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, VideoViewDto>
        {
            private readonly IVideoRepository videoRepository;
            private readonly VideoBusinessRules videoBusinessRules;
            public GetVideoByIdQueryHandler(IVideoRepository videoRepository, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<VideoViewDto> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
            {
                var video = await videoBusinessRules.VideoCheckById(request.Id);

                return new VideoViewDto
                {
                    Id = video.Id,
                    Title = video.Title,
                    VideoUrl = video.VideoUrl,
                    Description = video.Description,
                    WatchCount = video.WatchCount
                };
            }
        }

    }
}
