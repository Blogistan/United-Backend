using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Writer", "User" };

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;
            public DeleteOperationClaimCommandHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<DeleteOperationClaimResponse> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var operationClaim=await operationClaimBusinessRules.OperationClaimCheckById(request.Id);

                var deletedOperationClaim = await operationClaimRepostiory.DeleteAsync(operationClaim,true);

                var response = mapper.Map<DeleteOperationClaimResponse>(deletedOperationClaim);

                return response;
            }
        }
    }
}
