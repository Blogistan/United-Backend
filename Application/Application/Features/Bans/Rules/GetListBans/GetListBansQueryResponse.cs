using Application.Features.Bans.Dtos;

namespace Application.Features.Bans.Rules.GetListBans
{
    public class GetListBansQueryResponse
    {
        public ICollection<BanListViewDto> Items { get; set; }
    }
}
