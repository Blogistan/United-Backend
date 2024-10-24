using Core.Application.Responses;

namespace Application.Features.SiteUsers.Commands.DeleteSiteUser
{
    public record DeleteSiteUserCommandResponse :IResponse
    {
        public int Id { get; set; }
    }
}
