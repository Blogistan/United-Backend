﻿using Application.Features.Bans.Dtos;

namespace Application.Features.Bans.Queries.GetListBans
{
    public class GetListBansQueryResponse
    {
        public List<BanListViewDto> Items { get; set; }
    }
}
