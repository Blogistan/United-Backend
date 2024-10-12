using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Queries.GetListReportDynamic
{
    public class GetListReportDynamicQuery : IRequest<GetListReportDynamicQueryResponse>,ISecuredRequest
    {
        public DynamicQuery DynamicQuery { get; set; }
        public PageRequest PageRequest { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };

        public class GetListReportDynamicQueryHandler : IRequestHandler<GetListReportDynamicQuery, GetListReportDynamicQueryResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;
            public GetListReportDynamicQueryHandler(IReportRepository reportRepository, IMapper mapper)
            {
                this.reportRepository = reportRepository;
                this.mapper = mapper;
            }

            public async Task<GetListReportDynamicQueryResponse> Handle(GetListReportDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Report> paginate = await reportRepository.GetListByDynamicAsync(request.DynamicQuery, index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(x => x.ReportType).Include(x => x.SiteUser).ThenInclude(x=>x.User));

                GetListReportDynamicQueryResponse getListReportDynamicQueryResponse = mapper.Map<GetListReportDynamicQueryResponse>(paginate);

                return getListReportDynamicQueryResponse;
            }
        }
    }
}
