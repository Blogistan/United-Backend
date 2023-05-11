﻿using Amazon.Runtime.Internal;
using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.CreateVideo
{
    public class CreateVideoCommand : IRequest<CreateVideoResponse>
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount => 0;

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
                await videoBusinessRules.VideoCannotBeDuplicatedWhenInserted(request.Title,request.VideoUrl);

                Video video = new()
                {
                    Title = request.Title,
                    VideoUrl = request.VideoUrl,
                    Description = request.Description
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