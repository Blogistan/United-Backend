using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdateUserOperationClaimCommandResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;
            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                this.mapper = mapper;
            }

            public async Task<UpdateUserOperationClaimCommandResponse> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.UserOperationClaimCannotBeDuplicatedWhenUpdated(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.AddAsync(userClaim);

                var response = mapper.Map<UpdateUserOperationClaimCommandResponse>(createdUserClaim);

                return response;
            }
        }
    }
}
