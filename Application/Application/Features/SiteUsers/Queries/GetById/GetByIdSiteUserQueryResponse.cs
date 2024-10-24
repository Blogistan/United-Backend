using Application.Features.SiteUsers.Dtos;

namespace Application.Features.SiteUsers.Queries.GetById
{
    public record GetByIdSiteUserQueryResponse
    {
        public SiteUserListViewDto SiteUserListViewDto { get; set; }
    }
}
