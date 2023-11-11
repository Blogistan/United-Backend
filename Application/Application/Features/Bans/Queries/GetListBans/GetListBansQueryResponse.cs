using Application.Features.Bans.Dtos;

namespace Application.Features.Bans.Queries.GetListBans
{
    public class GetListBansQueryResponse
    {
        public ICollection<BanListViewDto> Items { get; set; }
    }
}
