using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimCommandResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }


        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            private readonly ISiteUserRepository siteUserRepository;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaim, ISiteUserRepository siteUserRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = userOperationClaim;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<CreateUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await businessRules.UserOperationClaimCannotBeDuplicatedWhenInserted(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.AddAsync(userClaim);

                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include => include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));
                var response = mapper.Map<CreateUserOperationClaimCommandResponse>(paginate);


                return response;

            }
        }
    }
}
