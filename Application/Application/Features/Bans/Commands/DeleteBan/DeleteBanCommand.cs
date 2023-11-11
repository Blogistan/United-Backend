using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Bans.Commands.DeleteBan
{
    public class DeleteBanCommand : IRequest<DeleteBanCommandResponse>
    {
        public Guid ReportID { get; set; }

        public class DeleteBanCommandHandler : IRequestHandler<DeleteBanCommand, DeleteBanCommandResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            public DeleteBanCommandHandler(IBanRepository banRepository, IMapper mapper)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
            }

            public async Task<DeleteBanCommandResponse> Handle(DeleteBanCommand request, CancellationToken cancellationToken)
            {
                var ban = await banRepository.GetAsync(x => x.Id == request.ReportID);

                var deletedBan = await banRepository.DeleteAsync(ban);

                DeleteBanCommandResponse deleteBanCommandResponse = mapper.Map<DeleteBanCommandResponse>(deletedBan);

                return deleteBanCommandResponse;
            }
        }
    }
}
