using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand : IRequest<CreateUserOperationClaimCommandResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }


        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules businessRules;
            private readonly ISiteUserRepository siteUserRepository;

            public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaim, ISiteUserRepository siteUserRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.mapper = mapper;
                this.businessRules = userOperationClaim;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<CreateUserOperationClaimCommandResponse> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await businessRules.UserOperationClaimCannotBeDuplicatedWhenInserted(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.AddAsync(userClaim);

                //IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(predicate: x => x.UserId == request.UserId, include: include => include.Include(x => x.OperationClaim).Include(x => x.User));


                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(predicate:x=>x.Id==request.UserId,include: include => include.Include(x => x.UserOperationClaims).ThenInclude(x=>x.OperationClaim));
                //var response = mapper.Map<CreateUserOperationClaimCommandResponse>(paginate);
                var claims = paginate.Items[0].UserOperationClaims.Select(x => x.OperationClaim).ToList();
                CreateUserOperationClaimCommandResponse response = new CreateUserOperationClaimCommandResponse
                {
                    Items = new UserOperationClaimListViewDto
                    {
                        UserID = paginate.Items[0].Id,
                        UserName = paginate.Items[0].FirstName + ' ' + paginate.Items[0].LastName,
                        Claims = mapper.Map<List<OperationClaimListViewDto>>(claims)
                   }
                };

                return response;

            }
        }
    }
}
