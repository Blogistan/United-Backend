﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bans.Rules.GetListBans
{
    public class GetListBansQuery : IRequest<GetListBansQueryResponse>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListBansQueryHandler : IRequestHandler<GetListBansQuery, GetListBansQueryResponse>
        {
            private readonly IBanRepository banRepository;
            private readonly IMapper mapper;
            public GetListBansQueryHandler(IBanRepository banRepository, IMapper mapper)
            {
                this.banRepository = banRepository;
                this.mapper = mapper;
            }

            public async Task<GetListBansQueryResponse> Handle(GetListBansQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Ban> paginate = await banRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize
                    , include: x => x.Include(x => x.User).Include(x => x.Report));

                var result = mapper.Map<GetListBansQueryResponse>(paginate);

                return result;
            }
        }
    }
}
