using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommand : IRequest<CreateVideoResponse>
    {
        public CreateVideoDto CreateVideoDto { get; set; }

        public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, CreateVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly VideoBusinessRules videoBusinessRules;
            public CreateVideoCommandHandler(IVideoRepository videoRepository, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<CreateVideoResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
            {
                await videoBusinessRules.VideoCannotBeDuplicatedWhenInserted(request.CreateVideoDto.Title,request.CreateVideoDto.VideoUrl);

                Video video = new()
                {
                    Title = request.CreateVideoDto.Title,
                    VideoUrl = request.CreateVideoDto.VideoUrl,
                    Description = request.CreateVideoDto.Description
                };

                Video CreatedVideo = await videoRepository.AddAsync(video);

                return new CreateVideoResponse
                {
                    Id = CreatedVideo.Id,
                    Description = CreatedVideo.Description,
                    Title = CreatedVideo.Title,
                    VideoUrl = CreatedVideo.VideoUrl,
                    WatchCount = CreatedVideo.WatchCount

                };
            }
        }
    }
}
