using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Queries.GetListContent
{
    public class GetListContentQuery : IRequest<GetListContentQueryResponse>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

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
