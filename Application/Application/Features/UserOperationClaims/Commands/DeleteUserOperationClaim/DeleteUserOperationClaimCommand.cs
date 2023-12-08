using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimResponse>
    {
        public int UserID { get; set; }
        public int OperationClaimId { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules businessRules)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = businessRules;
            }

            public async Task<DeleteUserOperationClaimResponse> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claim = await businessRules.UserOperationClaimCheckById(request.UserID, request.OperationClaimId);
                var deletedOperationClaim = await userOperationClaimRepository.DeleteAsync(claim);
                var response = mapper.Map<DeleteUserOperationClaimResponse>(deletedOperationClaim);

                return response;
            }
        }
    }
}
