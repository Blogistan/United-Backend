using Application.Features.SiteUsers.Dtos;
using Application.Features.SiteUsers.Rules;
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
            private readonly UserBusinessRules userBusinessRules;
            public GetByIdSiteUserQueryHandler(ISiteUserRepository siteUserRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<GetByIdSiteUserQueryResponse> Handle(GetByIdSiteUserQuery request, CancellationToken cancellationToken)
            {
                await userBusinessRules.UserIdShouldBeExistsWhenSelected(request.Id);
                SiteUser siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id));
                SiteUserListViewDto siteUserListViewDto = mapper.Map<SiteUserListViewDto>(siteUser);

                GetByIdSiteUserQueryResponse response = new() { SiteUserListViewDto = siteUserListViewDto };

                return response;
            }
        }
    }
}
