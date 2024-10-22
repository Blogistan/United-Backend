using Application.Features.Bans.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bans.Queries.GetListBans
{
    public record GetListBansQueryResponse :IResponse
    {
        public List<BanListViewDto> Items { get; set; }

        public GetListBansQueryResponse(List<BanListViewDto> items)
        {
            Items = items;
        }
    }
}
