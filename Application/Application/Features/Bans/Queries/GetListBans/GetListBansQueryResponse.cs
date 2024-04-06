using Application.Features.Bans.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bans.Queries.GetListBans
{
    public class GetListBansQueryResponse:IResponse
    {
        public List<BanListViewDto> Items { get; set; }

        public GetListBansQueryResponse(List<BanListViewDto> items)
        {
            Items = items;
        }
    }
}
