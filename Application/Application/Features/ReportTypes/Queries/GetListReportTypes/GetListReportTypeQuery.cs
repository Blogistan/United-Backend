using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReportTypes.Queries.GetListReportTypes
{
    public class GetListReportTypeQuery : IRequest<GetListReportTypeQueryResponse>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListReportTypeQueryHandler : IRequestHandler<GetListReportTypeQuery, GetListReportTypeQueryResponse>
        {
            private readonly IReportTypeRepository reportTypeRepository;
            private readonly IMapper mapper;
            public GetListReportTypeQueryHandler(IReportTypeRepository reportTypeRepository, IMapper mapper)
            {
                this.reportTypeRepository = reportTypeRepository;
                this.mapper = mapper;
            }

            public async Task<GetListReportTypeQueryResponse> Handle(GetListReportTypeQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ReportType> paginate = await reportTypeRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                var response = mapper.Map<GetListReportTypeQueryResponse>(paginate);

                return response;
            }
        }
    }
}
