using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.CreateRangeVideo
{
    public class CreateRangeVideoCommand : IRequest<CreateRangeVideoResponse>
    {
        public List<CreateVideoDto> CreateVideoDtos { get; set; }

        public class CreateRangeVideoCommandHandler : IRequestHandler<CreateRangeVideoCommand, CreateRangeVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly VideoBusinessRules videoBusinessRules;
            private readonly IMapper mapper;
            public CreateRangeVideoCommandHandler(IVideoRepository videoRepository, VideoBusinessRules videoBusinessRules, IMapper mapper)
            {
                this.videoRepository = videoRepository;
                this.videoBusinessRules = videoBusinessRules;
                this.mapper = mapper;
            }

            public async Task<CreateRangeVideoResponse> Handle(CreateRangeVideoCommand request, CancellationToken cancellationToken)
            {
                foreach (var item in request.CreateVideoDtos)
                {
                    await videoBusinessRules.VideoCannotBeDuplicatedWhenInserted(item.Title, item.VideoUrl);
                }
                ICollection<Video> videos = mapper.Map<ICollection<Video>>(request.CreateVideoDtos);

                var addeVideos = await videoRepository.AddRangeAsync(videos);

                List<VideoViewDto> videoListDto = mapper.Map<List<VideoViewDto>>(addeVideos);

                return new CreateRangeVideoResponse
                {
                    Items = videoListDto
                };
            }
        }
    }
}
