﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
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
            private readonly IUserRepository userRepository;
            public GetListUserOperationClaimByIdQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, IUserRepository userRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.userRepository = userRepository;
            }

            public async Task<GetListUserOperationClaimDynamicQueryResponse> Handle(GetListUserOperationClaimDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> paginate = await userRepository.GetListByDynamicAsync(dynamic:request.DynamicQuery,index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: include => include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));

                var response = mapper.Map<GetListUserOperationClaimDynamicQueryResponse>(paginate);

                return response;
            }
        }
    }
}
