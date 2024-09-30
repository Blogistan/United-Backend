using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Commands.DeleteBan
{
    public class DeleteBanCommand : IRequest<DeleteBanCommandResponse>
    {
        public Guid ReportID { get; set; }

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

                DeleteBanCommandResponse deleteBanCommandResponse = mapper.Map<DeleteBanCommandResponse>(deletedBan);

                return deleteBanCommandResponse;
            }
        }
    }
}
