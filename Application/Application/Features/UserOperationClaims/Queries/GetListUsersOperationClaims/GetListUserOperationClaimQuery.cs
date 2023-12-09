using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims
{
    public class GetListUserOperationClaimQuery : IRequest<GetListUserOperationClaimQueryResponse>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, GetListUserOperationClaimQueryResponse>
        {
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<GetListUserOperationClaimQueryResponse> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include => include.Include(x => x.OperationClaim).Include(x => x.User));

                var response = mapper.Map<GetListUserOperationClaimQueryResponse>(paginate);

                return response;
            }
        }
    }
}
