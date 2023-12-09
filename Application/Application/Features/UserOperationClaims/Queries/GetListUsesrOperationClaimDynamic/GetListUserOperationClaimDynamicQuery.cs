using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic
{
    public class GetListUserOperationClaimDynamicQuery : IRequest<GetListUserOperationClaimDynamicQueryResponse>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }

        public class GetListUserOperationClaimByIdQueryHandler : IRequestHandler<GetListUserOperationClaimDynamicQuery, GetListUserOperationClaimDynamicQueryResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            public GetListUserOperationClaimByIdQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
            }

            public async Task<GetListUserOperationClaimDynamicQueryResponse> Handle(GetListUserOperationClaimDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListByDynamicAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, dynamic: request.DynamicQuery, include: include => include.Include(x => x.OperationClaim).Include(x => x.User));

                var response = mapper.Map<GetListUserOperationClaimDynamicQueryResponse>(paginate);

                return response;
            }
        }
    }
}
