using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public class DeleteOperationClaimCommand : IRequest<DeleteOperationClaimResponse>
    {
        public int Id { get; set; }

        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeleteOperationClaimResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            public DeleteOperationClaimCommandHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
            }

            public async Task<DeleteOperationClaimResponse> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var operationClaim = await operationClaimRepostiory.GetAsync(x => x.Id == request.Id);

                var deletedOperationClaim = await operationClaimRepostiory.DeleteAsync(operationClaim);

                var response = mapper.Map<DeleteOperationClaimResponse>(deletedOperationClaim);

                return response;
            }
        }
    }
}
