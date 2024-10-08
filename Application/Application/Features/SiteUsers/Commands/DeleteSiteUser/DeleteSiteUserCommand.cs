﻿using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Commands.DeleteSiteUser
{
    public class DeleteSiteUserCommand : IRequest<DeleteSiteUserCommandResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin" };

        public class DeleteSiteUserCommandHandler : IRequestHandler<DeleteSiteUserCommand, DeleteSiteUserCommandResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly UserBusinessRules userBusinessRules;

            public DeleteSiteUserCommandHandler(ISiteUserRepository siteUserRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<DeleteSiteUserCommandResponse> Handle(DeleteSiteUserCommand request, CancellationToken cancellationToken)
            {
                SiteUser? siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id));

                await userBusinessRules.UserShouldBeExistsWhenSelected(siteUser.User);

                await siteUserRepository.DeleteAsync(siteUser);

                DeleteSiteUserCommandResponse response = mapper.Map<DeleteSiteUserCommandResponse>(siteUser);
                return response;
            }
        }
    }
}
