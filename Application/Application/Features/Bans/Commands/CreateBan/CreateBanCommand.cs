using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.CreateBan
{
    public class CreateBanCommand : IRequest<CreateBanCommandResponse>, ISecuredRequest
    {
        public int SiteUserId { get; set; }
        public int ReportID { get; set; }
        public bool IsPerma { get; set; }
        private int CreateUser { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
        string[] ISecuredRequest.Roles => ["Admin", "Moderator"];

        public class CreateBanCommandHandler : IRequestHandler<CreateBanCommand, CreateBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly IRefreshTokenRepository refreshTokenRepository;
            public CreateBanCommandHandler(IBanRepository banRepository, IMapper mapper, ISiteUserRepository siteUserRepository, IRefreshTokenRepository refreshTokenRepository)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
                this.refreshTokenRepository = refreshTokenRepository;
            }

            public async Task<CreateBanCommandResponse> Handle(CreateBanCommand request, CancellationToken cancellationToken)
            {
                Ban ban = mapper.Map<Ban>(request);

                Ban AddedBan = await banRepository.AddAsync(ban);

                var user = await siteUserRepository.GetAsync(x => x.Id == AddedBan.SiteUserId, include: x => x.Include(x => x.User));
                if (request.IsPerma)
                    user.User.IsActive = false;

                await siteUserRepository.UpdateAsync(user);
                var createdBan = await banRepository.GetAsync(x => x.Id == AddedBan.Id, include: x => x.Include(x => x.SiteUser).ThenInclude(x => x.User));
                //var response = mapper.Map<CreateBanCommandResponse>(createdBan);

                var activeTokens = await refreshTokenRepository.GetListAsync(predicate:x=>x.UserId==user.Id && x.Expires>DateTime.UtcNow);

                await refreshTokenRepository.DeleteRangeAsync(activeTokens.Items);

                CreateBanCommandResponse response = new()
                {
                    Id = createdBan.Id,
                    User = new() { Id = createdBan.Id, FirstName = createdBan.SiteUser.User.FirstName, LastName = createdBan.SiteUser.User.LastName, Biography = createdBan.SiteUser.Biography, Email = createdBan.SiteUser.User.Email, IsVerified = (bool)createdBan.SiteUser.IsVerified, ProfileImageUrl = createdBan.SiteUser.ProfileImageUrl },
                    BanDetail = createdBan.BanDetail,
                    BanStartDate = createdBan.BanStartDate,
                    BanEndDate = createdBan.BanEndDate,
                    IsPerma = createdBan.IsPerma
                };
                return response;

            }
        }

    }
}
