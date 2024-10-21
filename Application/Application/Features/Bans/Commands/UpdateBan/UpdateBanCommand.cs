using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public  class UpdateBanCommand:IRequest<UpdateBanCommandResponse>,ISecuredRequest
    {
        public int Id { get; set; }
        public int ReportID { get; set; }
        public int UserId { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
        string[] ISecuredRequest.Roles => ["Admin", "Moderator"];

        public class UpdateBanCommandHandler:IRequestHandler<UpdateBanCommand, UpdateBanCommandResponse>
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

                var user = await siteUserRepository.GetAsync(x => x.Id == updatedBan.SiteUserId,include:x=>x.Include(x=>x.User));
                user.User.IsActive = request.IsPerma;

                await siteUserRepository.UpdateAsync(user);

                UpdateBanCommandResponse response = mapper.Map<UpdateBanCommandResponse>(updatedBan);

                return response;
            }
        }
    }
}
