using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.UpdateRangeVideo
{
    public class UpdateRangeVideoCommand : IRequest<UpdateRangeVideoResponse>
    {
        public List<UpdateVideoDto> updateVideoDtos { get; set; }

        public class UpdateRangeVideoCommandHandler : IRequestHandler<UpdateRangeVideoCommand, UpdateRangeVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly IMapper mapper;
            private readonly VideoBusinessRules videoBusinessRules;
            public UpdateRangeVideoCommandHandler(IVideoRepository videoRepository, IMapper mapper, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.mapper = mapper;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<UpdateRangeVideoResponse> Handle(UpdateRangeVideoCommand request, CancellationToken cancellationToken)
            {
                //await videoBusinessRules.VideoCannotBeDuplicatedWhenUpdated(request.updateVideoDtos);

                List<Video> videos = mapper.Map<List<Video>>(request.updateVideoDtos);

                var updatedVideos = await videoRepository.UpdateRangeAsync(videos);

                List<VideoViewDto> videoViewDtos = mapper.Map<List<VideoViewDto>>(updatedVideos);

                return new UpdateRangeVideoResponse
                {
                    Items = videoViewDtos
                };

            }
        }
    }
}
