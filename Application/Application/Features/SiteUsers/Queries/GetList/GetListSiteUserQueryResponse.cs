
using Application.Features.SiteUsers.Dtos;

namespace Application.Features.SiteUsers.Queries.GetList
{
	public class GetListSiteUserQueryResponse
    {
        public IList<SiteUserListViewDto> Items { get; set; }
    }
}
