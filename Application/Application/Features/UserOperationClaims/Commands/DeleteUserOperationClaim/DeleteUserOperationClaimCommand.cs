using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            private readonly ISiteUserRepository siteUserRepository;
            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules businessRules, ISiteUserRepository siteUserRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = businessRules;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<DeleteUserOperationClaimResponse> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claim = await businessRules.UserOperationClaimCheckById(request.UserId, request.OperationClaimId);
                var deletedOperationClaim = await userOperationClaimRepository.DeleteAsync(claim,true);
                //var response = mapper.Map<DeleteUserOperationClaimResponse>(deletedOperationClaim);

                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include => 
                include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                var response = mapper.Map<DeleteUserOperationClaimResponse>(paginate);
              
                return response;
            }
        }
    }
}
