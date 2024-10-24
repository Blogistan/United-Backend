﻿using Application.Features.Bans.Dtos;
using Core.Application.Responses;

namespace Application.Features.Bans.Queries.GetListBansDynamic
{
    public record GetListBansDynamicQueryResponse : IResponse
    {
        public List<BanListViewDto> Items { get; set; }

        public GetListBansDynamicQueryResponse(List<BanListViewDto> items)
        {
            Items = items;
        }
    }
}
