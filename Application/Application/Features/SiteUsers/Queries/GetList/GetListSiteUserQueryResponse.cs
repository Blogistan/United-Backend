
using Application.Features.SiteUsers.Dtos;

namespace Application.Features.SiteUsers.Queries.GetList
{
	public record GetListSiteUserQueryResponse
    {
        public IList<SiteUserListViewDto> Items { get; set; }
    }
}
