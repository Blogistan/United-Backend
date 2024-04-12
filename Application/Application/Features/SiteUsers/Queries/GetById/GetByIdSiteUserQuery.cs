using Application.Features.SiteUsers.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Queries.GetById
{
    public class GetByIdSiteUserQuery : IRequest<GetByIdSiteUserQueryResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin" };


        public class GetByIdSiteUserQueryHandler : IRequestHandler<GetByIdSiteUserQuery, GetByIdSiteUserQueryResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            public GetByIdSiteUserQueryHandler(ISiteUserRepository siteUserRepository, IMapper mapper)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
            }

            public async Task<GetByIdSiteUserQueryResponse> Handle(GetByIdSiteUserQuery request, CancellationToken cancellationToken)
            {
                SiteUser siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id));
                SiteUserListViewDto siteUserListViewDto = mapper.Map<SiteUserListViewDto>(siteUser);

                GetByIdSiteUserQueryResponse response = new() { SiteUserListViewDto = siteUserListViewDto };

                return response;
            }
        }
    }
}
