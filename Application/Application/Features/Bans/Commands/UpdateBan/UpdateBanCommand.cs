using Application.Features.Bans.Commands.CreateBan;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public class UpdateBanCommand : IRequest<UpdateBanCommandResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public int ReportID { get; set; }
        public int UserId { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
        string[] ISecuredRequest.Roles => ["Admin", "Moderator"];

        public class UpdateBanCommandHandler : IRequestHandler<UpdateBanCommand, UpdateBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            private readonly ISiteUserRepository siteUserRepository;
            public UpdateBanCommandHandler(IBanRepository banRepository, IMapper mapper, ISiteUserRepository siteUserRepository)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<UpdateBanCommandResponse> Handle(UpdateBanCommand request, CancellationToken cancellationToken)
            {
                var ban = mapper.Map<Ban>(request);

                Ban updatedBan = await banRepository.UpdateAsync(ban);

                var user = await siteUserRepository.GetAsync(x => x.Id == updatedBan.SiteUserId, include: x => x.Include(x => x.User));
                user.User.IsActive = request.IsPerma;

                await siteUserRepository.UpdateAsync(user);

                //UpdateBanCommandResponse response = mapper.Map<UpdateBanCommandResponse>(updatedBan);
                var updatedBanResponse = await banRepository.GetAsync(x => x.Id == updatedBan.Id, include: x => x.Include(x => x.SiteUser).ThenInclude(x => x.User));
                UpdateBanCommandResponse response = new()
                {
                    Id = updatedBanResponse.Id,
                    User = new() { Id = updatedBanResponse.Id, FirstName = updatedBanResponse.SiteUser.User.FirstName, LastName = updatedBanResponse.SiteUser.User.LastName, Biography = updatedBanResponse.SiteUser.Biography, Email = updatedBanResponse.SiteUser.User.Email, IsVerified = (bool)updatedBanResponse.SiteUser.IsVerified, ProfileImageUrl = updatedBanResponse.SiteUser.ProfileImageUrl },
                    BanDetail = updatedBanResponse.BanDetail,
                    BanStartDate = updatedBanResponse.BanStartDate,
                    BanEndDate = updatedBanResponse.BanEndDate,
                    IsPerma = updatedBanResponse.IsPerma
                };

                return response;
            }
        }
    }
}
