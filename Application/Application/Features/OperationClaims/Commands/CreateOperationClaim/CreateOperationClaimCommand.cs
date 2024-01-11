using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommand : IRequest<CreateOperationClaimResponse>, ISecuredRequest
    {
        public string Name { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Writer", "User" };

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            private readonly OperationClaimBusinessRules operationClaimBusinessRules;
            public CreateOperationClaimCommandHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
                this.operationClaimBusinessRules = operationClaimBusinessRules;
            }
            public async Task<CreateOperationClaimResponse> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await operationClaimBusinessRules.OperationClaimCannotBeDuplicatedWhenInserted(request.Name);

                var claim = mapper.Map<OperationClaim>(request);

                var addedClaim = await operationClaimRepostiory.AddAsync(claim);

                var response = mapper.Map<CreateOperationClaimResponse>(addedClaim);

                return response;
            }
        }
    }
}
