using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Videos.Queries.GetListVideo
{
    public class GetListVideoQuery:IRequest<CategoryListDto>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListVideoQueryHandler:IRequestHandler<GetListVideoQuery, CategoryListDto>
        {
            private readonly IVideoRepository videoRepository;
            private readonly IMapper mapper;
            public GetListVideoQueryHandler(IVideoRepository videoRepository, IMapper mapper)
            {
                this.videoRepository = videoRepository;
                this.mapper = mapper;
            }

            public async Task<CategoryListDto> Handle(GetListVideoQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Video> paginate = await videoRepository.GetListAsync(index:request.PageRequest.Page,size:request.PageRequest.PageSize);

                CategoryListDto categoryListDto = mapper.Map<CategoryListDto>(paginate);

                return categoryListDto;
            }
        }
    }
}
