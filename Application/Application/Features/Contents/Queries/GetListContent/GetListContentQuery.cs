using Amazon.Runtime.Internal;
using Application.Features.Contents.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Queries.GetListContent
{
    public class GetListContentQuery : IRequest<GetListContentQueryResponse>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListContentQueryHandler : IRequestHandler<GetListContentQuery, GetListContentQueryResponse>
        {
            private readonly IContentRepository contentRepository;
            private readonly IMapper mapper;
            public GetListContentQueryHandler(IContentRepository contentRepository, IMapper mapper)
            {
                this.contentRepository = contentRepository;
                this.mapper = mapper;
            }

            public async Task<GetListContentQueryResponse> Handle(GetListContentQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Content> paginate = await contentRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                GetListContentQueryResponse contentListViewDto = mapper.Map<GetListContentQueryResponse>(paginate);

                return contentListViewDto;
            }
        }
    }
}
