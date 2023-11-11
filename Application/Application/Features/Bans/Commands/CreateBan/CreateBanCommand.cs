using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bans.Commands.CreateBan
{
    public class CreateBanCommand : IRequest<CreateBanCommandResponse>
    {
        public int UserID { get; set; }
        public int ReportID { get; set; }
        public bool IsPerma { get; set; }
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string? BanDetail { get; set; } = string.Empty;

        public class CreateBanCommandHandler : IRequestHandler<CreateBanCommand, CreateBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            public CreateBanCommandHandler(IBanRepository banRepository, IMapper mapper)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
            }

            public async Task<CreateBanCommandResponse> Handle(CreateBanCommand request, CancellationToken cancellationToken)
            {
                Ban ban = mapper.Map<Ban>(request);

                Ban AddedBan = await banRepository.AddAsync(ban);

                var response = mapper.Map<CreateBanCommandResponse>(AddedBan);

                return response;

            }
        }

    }
}
