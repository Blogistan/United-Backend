using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimResponse>,ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            private readonly IUserRepository userRepository;
            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules businessRules, IUserRepository userRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = businessRules;
                this.userRepository = userRepository;
            }

            public async Task<DeleteUserOperationClaimResponse> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claim = await businessRules.UserOperationClaimCheckById(request.UserId, request.OperationClaimId);
                var deletedOperationClaim = await userOperationClaimRepository.DeleteAsync(claim,true);
                //var response = mapper.Map<DeleteUserOperationClaimResponse>(deletedOperationClaim);

                IPaginate<User> paginate = await userRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include => 
                include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                var response = mapper.Map<DeleteUserOperationClaimResponse>(paginate);
              
                return response;
            }
        }
    }
}
