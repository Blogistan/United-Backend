﻿using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimCommandResponse>
    {
        public int UserID { get; set; }
        public int OpreationClaimID { get; set; }


        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaim)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = userOperationClaim;
            }

            public async Task<CreateUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await businessRules.UserOperationClaimCannotBeDuplicatedWhenInserted(request.UserID, request.OpreationClaimID);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.AddAsync(userClaim);

                var response = mapper.Map<CreateUserOperationClaimCommandResponse>(createdUserClaim);

                return response;

            }
        }
    }
}
