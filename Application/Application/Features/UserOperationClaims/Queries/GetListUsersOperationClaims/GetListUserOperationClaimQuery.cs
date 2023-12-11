using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
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
            private readonly ISiteUserRepository siteUserRepository;
            public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, ISiteUserRepository siteUserRepository)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<GetListUserOperationClaimQueryResponse> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                //IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include => include.Include(x => x.OperationClaim).Include(x => x.User));


                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include=>include.Include(x=>x.UserOperationClaims));

                var response = mapper.Map<GetListUserOperationClaimQueryResponse>(paginate);
                
                return response;
            }
        }
    }
}
