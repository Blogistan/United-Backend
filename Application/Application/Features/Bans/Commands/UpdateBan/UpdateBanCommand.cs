using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public  class UpdateBanCommand:IRequest<UpdateBanCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid ReportID { get; set; }
        public int UserId { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

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

                var user = await siteUserRepository.GetAsync(x => x.Id == updatedBan.UserID);
                user.IsActive = request.IsPerma;

                await siteUserRepository.UpdateAsync(user);

                UpdateBanCommandResponse response = mapper.Map<UpdateBanCommandResponse>(updatedBan);

                return response;
            }
        }
    }
}
