using Application.Features.Bans.Commands.UpdateBan;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.DeleteBan
{
    public class DeleteBanCommand : IRequest<DeleteBanCommandResponse>,ISecuredRequest
    {
        public int ReportID { get; set; }
        string[] ISecuredRequest.Roles => ["Admin", "Moderator"];

        public class DeleteBanCommandHandler : IRequestHandler<DeleteBanCommand, DeleteBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            public DeleteBanCommandHandler(IBanRepository banRepository, IMapper mapper, ISiteUserRepository siteUserRepository)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<DeleteBanCommandResponse> Handle(DeleteBanCommand request, CancellationToken cancellationToken)
            {
                var ban = await banRepository.GetAsync(x => x.Id == request.ReportID);

                var user = await siteUserRepository.GetAsync(x => x.Id == ban.SiteUserId,include:x=>x.Include(x=>x.User));
                user.User.IsActive = true;
                await siteUserRepository.UpdateAsync(user);
                var deletedBan = await banRepository.DeleteAsync(ban);
                var deletedBanResponse = await banRepository.GetAsync(x => x.Id == deletedBan.Id, include: x => x.Include(x => x.SiteUser).ThenInclude(x => x.User));
                //DeleteBanCommandResponse deleteBanCommandResponse = mapper.Map<DeleteBanCommandResponse>(deletedBan);

                DeleteBanCommandResponse response = new()
                {
                    Id = deletedBanResponse.Id,
                    User = new() { Id = deletedBanResponse.Id, FirstName = deletedBanResponse.SiteUser.User.FirstName, LastName = deletedBanResponse.SiteUser.User.LastName, Biography = deletedBanResponse.SiteUser.Biography, Email = deletedBanResponse.SiteUser.User.Email, IsVerified = (bool)deletedBanResponse.SiteUser.IsVerified, ProfileImageUrl = deletedBanResponse.SiteUser.ProfileImageUrl },
                    BanDetail = deletedBanResponse.BanDetail,
                    BanStartDate = deletedBanResponse.BanStartDate,
                    BanEndDate = deletedBanResponse.BanEndDate,
                    IsPerma = deletedBanResponse.IsPerma
                };

                return response;
            }
        }
    }
}
