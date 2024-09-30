using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdateUserOperationClaimCommandResponse>,ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;
            private readonly IUserRepository userRepository;
            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper, IUserRepository userRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                this.mapper = mapper;
                this.userRepository = userRepository;
            }

            public async Task<UpdateUserOperationClaimCommandResponse> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.UserOperationClaimCannotBeDuplicatedWhenUpdated(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.UpdateAsync(userClaim);

                IPaginate<User> paginate = await userRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include =>
                include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                var response = mapper.Map<UpdateUserOperationClaimCommandResponse>(paginate);
          
                return response;
            }
        }
    }
}
