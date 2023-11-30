using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Auth.Commands.OperationClaim
{
    public class CreateOperationClaimCommand : IRequest<CreateOperationClaimResponse>, ISecuredRequest
    {
        public string[] Roles => new string[] {"Admin","Moderator","Writer","User"};

        public class CreateOperationClaimCommandHandler:IRequestHandler<CreateOperationClaimCommand, CreateOperationClaimResponse>
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
                throw new NotImplementedException();
            }
        }
    }
}
