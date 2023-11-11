using Application.Features.Bans.Dtos;

namespace Application.Features.Bans.Queries.GetListBansDynamic
{
    public class GetListBansDynamicQueryResponse
    {
        public List<BanListViewDto> Items { get; set; }
    }
}
