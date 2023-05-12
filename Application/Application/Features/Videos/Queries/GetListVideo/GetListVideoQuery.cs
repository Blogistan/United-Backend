using Application.Features.Videos.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Queries.GetListVideo
{
    public class GetListVideoQuery:IRequest<VideoListDto>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListVideoQueryHandler:IRequestHandler<GetListVideoQuery, VideoListDto>
        {
            private readonly IVideoRepository videoRepository;
            private readonly IMapper mapper;
            public GetListVideoQueryHandler(IVideoRepository videoRepository, IMapper mapper)
            {
                this.videoRepository = videoRepository;
                this.mapper = mapper;
            }

            public async Task<VideoListDto> Handle(GetListVideoQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Video> paginate = await videoRepository.GetListAsync(index:request.PageRequest.Page,size:request.PageRequest.PageSize,withDeleted:false);

                VideoListDto videoListDto = mapper.Map<VideoListDto>(paginate);

                return videoListDto;
            }
        }
    }
}
