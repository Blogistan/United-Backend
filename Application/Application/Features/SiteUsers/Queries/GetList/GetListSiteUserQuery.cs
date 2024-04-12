using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Queries.GetList
{
    public class GetListSiteUserQuery : IRequest<GetListSiteUserQueryResponse>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin" };

        public GetListSiteUserQuery()
        {
            PageRequest = new PageRequest { Page = 0, PageSize = 10 };
        }
        public GetListSiteUserQuery(PageRequest pageRequest)
        {
            PageRequest = pageRequest;
        }

        public class GetListSiteUserQueryHandler : IRequestHandler<GetListSiteUserQuery, GetListSiteUserQueryResponse>
        {
            private readonly IMapper mapper;
            private readonly ISiteUserRepository siteUserRepository;

            public GetListSiteUserQueryHandler(IMapper mapper, ISiteUserRepository siteUserRepository)
            {
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<GetListSiteUserQueryResponse> Handle(GetListSiteUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, enableTracking: false,
                cancellationToken: cancellationToken);

                GetListSiteUserQueryResponse response = mapper.Map<GetListSiteUserQueryResponse>(paginate);

                return response;
            }
        }
    }
}
