using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.CreateBan
{
    public class CreateBanCommand : IRequest<CreateBanCommandResponse>,ISecuredRequest
    {
        public int SiteUserId { get; set; }
        public int ReportID { get; set; }
        public bool IsPerma { get; set; }
        private int CreateUser { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;
        public string[] Roles => ["Admin", "Moderator"];

        public class CreateBanCommandHandler : IRequestHandler<CreateBanCommand, CreateBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            public CreateBanCommandHandler(IBanRepository banRepository, IMapper mapper, ISiteUserRepository siteUserRepository)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
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
                var response = mapper.Map<CreateBanCommandResponse>(createdBan);

                return response;

            }
        }

    }
}
