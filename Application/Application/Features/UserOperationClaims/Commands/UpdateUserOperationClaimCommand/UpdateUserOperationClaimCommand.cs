using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommand : IRequest<UpdateUserOperationClaimCommandResponse>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdateUserOperationClaimCommandResponse>
        {
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            private readonly IMapper mapper;
            private readonly UserOperationClaimBusinessRules userOperationClaimBusinessRules;
            private readonly ISiteUserRepository siteUserRepository;
            public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper, ISiteUserRepository siteUserRepository)
            {
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<UpdateUserOperationClaimCommandResponse> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await userOperationClaimBusinessRules.UserOperationClaimCannotBeDuplicatedWhenUpdated(request.UserId, request.OperationClaimId);
                var userClaim = mapper.Map<UserOperationClaim>(request);

                var createdUserClaim = await userOperationClaimRepository.UpdateAsync(userClaim);

                IPaginate<SiteUser> paginate = await siteUserRepository.GetListAsync(predicate: x => x.Id == request.UserId, include: include =>
                include.Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim));
                //var response = mapper.Map<CreateUserOperationClaimCommandResponse>(paginate);
                var claims = paginate.Items[0].UserOperationClaims.Select(x => x.OperationClaim).ToList();
                UpdateUserOperationClaimCommandResponse response = new UpdateUserOperationClaimCommandResponse
                {

                    UserId = paginate.Items[0].Id,
                    UserName = paginate.Items[0].FirstName + ' ' + paginate.Items[0].LastName,
                    Claims = mapper.Map<List<OperationClaimListViewDto>>(claims)

                };

                return response;
            }
        }
    }
}
