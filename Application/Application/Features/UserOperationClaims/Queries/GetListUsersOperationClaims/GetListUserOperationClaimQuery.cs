using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims
{
    public class GetListUserOperationClaimQuery : IRequest<GetListUserOperationClaimQueryResponse>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        string[] ISecuredRequest.Roles => ["Admin", "Moderator"];

        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, GetListUserOperationClaimQueryResponse>
        {
            private readonly IMapper mapper;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IUserRepository userRepository;
            public GetListUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository)
            {
                this.mapper = mapper;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userRepository = userRepository;
            }

            public async Task<GetListUserOperationClaimQueryResponse> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                //IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include => include.Include(x => x.OperationClaim).Include(x => x.User));


                IPaginate<User> paginate = await userRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include=>include.Include(x=>x.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                var response = mapper.Map<GetListUserOperationClaimQueryResponse>(paginate);
                
                return response;
            }
        }
    }
}
