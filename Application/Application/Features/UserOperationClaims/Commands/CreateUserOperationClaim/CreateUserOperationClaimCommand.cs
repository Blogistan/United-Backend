using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimCommandResponse>,ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };


        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            private readonly IUserRepository userRepository;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaim, IUserRepository userRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = userOperationClaim;
                this.userRepository = userRepository;
            }

            public async Task<CreateUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await businessRules.UserOperationClaimCannotBeDuplicatedWhenInserted(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.AddAsync(userClaim);

                IPaginate<User> paginate = await userRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include => include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));
                var response = mapper.Map<CreateUserOperationClaimCommandResponse>(paginate);


                return response;

            }
        }
    }
}
