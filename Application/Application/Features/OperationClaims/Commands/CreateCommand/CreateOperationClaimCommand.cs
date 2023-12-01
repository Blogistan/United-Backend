using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.CreateCommand
{
    public class CreateOperationClaimCommand : IRequest<CreateOperationClaimResponse>, ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles => new string[] { "Admin", "Moderator", "Writer", "User" };

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            public CreateOperationClaimCommandHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
            }
            //To Be Contiuned
            public async Task<CreateOperationClaimResponse> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claim = mapper.Map<OperationClaim>(request);

                var addedClaim = await operationClaimRepostiory.AddAsync(claim);

                var response = mapper.Map<CreateOperationClaimResponse>(addedClaim);

                return response;
            }
        }
    }
}
