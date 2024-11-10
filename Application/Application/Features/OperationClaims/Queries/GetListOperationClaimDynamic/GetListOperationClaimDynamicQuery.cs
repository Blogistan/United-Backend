using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaimDynamic
{
    public class GetListOperationClaimDynamicQuery: IRequest<GetListOperationClaimDynamicQueryResponse>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin"};

        public class GetListOperationClaimDynamicQueryHandler : IRequestHandler<GetListOperationClaimDynamicQuery, GetListOperationClaimDynamicQueryResponse>
        {
            private readonly IOperationClaimRepostiory operationClaimRepostiory;
            private readonly IMapper mapper;
            public GetListOperationClaimDynamicQueryHandler(IOperationClaimRepostiory operationClaimRepostiory, IMapper mapper)
            {
                this.operationClaimRepostiory = operationClaimRepostiory;
                this.mapper = mapper;
            }

            public async Task<GetListOperationClaimDynamicQueryResponse> Handle(GetListOperationClaimDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OperationClaim> paginate = await operationClaimRepostiory.GetListByDynamicAsync(dynamic:request.DynamicQuery,index:request.PageRequest.Page,size:request.PageRequest.PageSize);

                var response = mapper.Map<GetListOperationClaimDynamicQueryResponse>(paginate);

                return response;
            }
        }
    }
}

