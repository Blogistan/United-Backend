using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.NewFolder.GetListOperationClaim
{
    public class GetListOperationClaimQuery : IRequest<GetListOperationClaimQueryResponse>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, GetListOperationClaimQueryResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            public GetListOperationClaimQueryHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
            }

            public async Task<GetListOperationClaimQueryResponse> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> paginate = await operationClaimRepostiory.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                var response = mapper.Map<GetListOperationClaimQueryResponse>(paginate);

                return response;
            }
        }
    }
}
