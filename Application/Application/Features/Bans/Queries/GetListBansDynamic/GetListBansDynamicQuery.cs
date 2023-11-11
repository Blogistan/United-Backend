using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bans.Queries.GetListBansDynamic
{
    public class GetListBansDynamicQuery : IRequest<GetListBansDynamicQueryResponse>
    {
        public DynamicQuery DynamicQuery { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListBansDynamicQueryHandler : IRequestHandler<GetListBansDynamicQuery, GetListBansDynamicQueryResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            public GetListBansDynamicQueryHandler(IBanRepository banRepository, IMapper mapper)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
            }

            public async Task<GetListBansDynamicQueryResponse> Handle(GetListBansDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Ban> paginate = await banRepository.GetListByDynamicAsync(request.DynamicQuery, index: request.PageRequest.Page, size: request.PageRequest.Page);

                GetListBansDynamicQueryResponse response = mapper.Map<GetListBansDynamicQueryResponse>(paginate);

                return response;
            }
        }
    }
}
