using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bans.Commands.UpdateBan
{
    public  class UpdateBanCommand:IRequest<UpdateBanCommandResponse>
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

        public class UpdateBanCommandHandler:IRequestHandler<UpdateBanCommand, UpdateBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            public UpdateBanCommandHandler(IBanRepository banRepository, IMapper mapper)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
            }

            public async Task<UpdateBanCommandResponse> Handle(UpdateBanCommand request, CancellationToken cancellationToken)
            {
                var ban = mapper.Map<Ban>(request);

                Ban updatedBan = await banRepository.UpdateAsync(ban);

                UpdateBanCommandResponse response = mapper.Map<UpdateBanCommandResponse>(updatedBan);

                return response;
            }
        }
    }
}
