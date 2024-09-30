using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Queries.GetListReport
{
    public class GetListReportQuery : IRequest<GetListReportQueryResponse>,ISecuredRequest 
    {
        public PageRequest PageRequest { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator" };

        public class GetListReportQueryHandler : IRequestHandler<GetListReportQuery, GetListReportQueryResponse>
        {
            private readonly IReportRepository reportRepository;
            private readonly IMapper mapper;

            public GetListReportQueryHandler(IReportRepository reportRepository, IMapper mapper)
            {
                this.reportRepository = reportRepository;
                this.mapper = mapper;
            }

            public async Task<GetListReportQueryResponse> Handle(GetListReportQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Report> paginate = await reportRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(x => x.ReportType).Include(x => x.SiteUser).ThenInclude(x=>x.User));

                var result = mapper.Map<GetListReportQueryResponse>(paginate);


                return result;
            }
        }
    }
}
