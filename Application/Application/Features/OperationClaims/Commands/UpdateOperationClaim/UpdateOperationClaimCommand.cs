using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand : IRequest<UpdateOperationClaimCommandResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Writer", "User" };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdateOperationClaimCommandResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<UpdateOperationClaimCommandResponse> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await operationClaimBusinessRules.OperationClaimCheckById(request.Id);
                await operationClaimBusinessRules.BlogCannotBeDuplicatedWhenUpdated(request.Name);
                var operationClaim = mapper.Map<OperationClaim>(request);

                var updatedOperationClaim = await operationClaimRepostiory.UpdateAsync(operationClaim);

                var response = mapper.Map<UpdateOperationClaimCommandResponse>(updatedOperationClaim);

                return response;
            }
        }
    }
}
