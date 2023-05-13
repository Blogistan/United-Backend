using Application.Features.Videos.Dtos;
using Application.Features.Videos.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Commands.DeleteRangeVideo
{
    public class DeleteRangeVideoCommand : IRequest<DeleteRangeVideoResponse>
    {
        public List<DeleteRangeVideoDto> DeleteRangeDto { get; set; }
        public bool Permanent { get; set; }

        public class DeleteRangeVideoCommandHandler : IRequestHandler<DeleteRangeVideoCommand, DeleteRangeVideoResponse>
        {
            private readonly IVideoRepository videoRepository;
            private readonly IMapper mapper;
            private readonly VideoBusinessRules videoBusinessRules;
            public DeleteRangeVideoCommandHandler(IVideoRepository videoRepository, IMapper mapper, VideoBusinessRules videoBusinessRules)
            {
                this.videoRepository = videoRepository;
                this.mapper = mapper;
                this.videoBusinessRules = videoBusinessRules;
            }

            public async Task<DeleteRangeVideoResponse> Handle(DeleteRangeVideoCommand request, CancellationToken cancellationToken)
            {
                var videos = await videoBusinessRules.VideoCheckById(request.DeleteRangeDto);

                ICollection<Video> deletedVidoes = await videoRepository.DeleteRangeAsync(videos,request.Permanent);

                List<VideoViewDto> videoViewDtos = mapper.Map<List<VideoViewDto>>(deletedVidoes);

                return new DeleteRangeVideoResponse { Items = videoViewDtos };


            }
        }
    }
}
