using Application.Features.Categories.Dtos;
using Application.Features.Videos.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Queries.GetListVideoDynamic
{
    public class GetListVideoDynamicQuery:IRequest<VideoListDto>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }

        public class GetListVideoDynamicQueryHandler:IRequestHandler<GetListVideoDynamicQuery, VideoListDto>
        {
            private readonly IVideoRepository videoRepository;
            private readonly IMapper mapper;
            public GetListVideoDynamicQueryHandler(IVideoRepository videoRepository, IMapper mapper)
            {
                this.videoRepository = videoRepository;
                this.mapper = mapper;
            }

            public async Task<VideoListDto> Handle(GetListVideoDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Video> paginate = await videoRepository.GetListByDynamicAsync(dynamic:request.DynamicQuery,index:request.PageRequest.Page,size:request.PageRequest.PageSize);

                VideoListDto videoListDto = mapper.Map<VideoListDto>(paginate);

                return videoListDto;
            }
        }
    }
}
