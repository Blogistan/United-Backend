using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Queries.GetListContentDynamic
{
    public class GetListContentDynamicQuery : IRequest<GetListContentDynamicQueryResponse>, ISecuredRequest
    {
        public DynamicQuery? DynamicQuery { get; set; }
        public PageRequest? PageRequest { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "User", "Admin" };


        public class GetListContentDynamicQueryHandler : IRequestHandler<GetListContentDynamicQuery, GetListContentDynamicQueryResponse>
        {
            private readonly IContentRepository contentRepository;
            private readonly IMapper mapper;
            public GetListContentDynamicQueryHandler(IContentRepository contentRepository, IMapper mapper)
            {
                this.contentRepository = contentRepository;
                this.mapper = mapper;
            }

            public async Task<GetListContentDynamicQueryResponse> Handle(GetListContentDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Content> paginate = await contentRepository.GetListByDynamicAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, dynamic: request.DynamicQuery);

                GetListContentDynamicQueryResponse contentListViewDto = mapper.Map<GetListContentDynamicQueryResponse>(paginate);

                return contentListViewDto;
            }
        }
    }
}
