using Core.Application.Responses;

namespace Application.Features.SiteUsers.Commands.DeleteSiteUser
{
    public class DeleteSiteUserCommandResponse:IResponse
    {
        public int Id { get; set; }
    }
}
