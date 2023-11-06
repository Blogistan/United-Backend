using Application.Features.ReportTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ReportTypes.Commands.DeleteReportType
{
    public class DeleteReportTypeCommand : IRequest<DeleteReportTypeCommandResponse>
    {
        public int Id { get; set; }

        public class DeleteReportTypeCommandHandler : IRequestHandler<DeleteReportTypeCommand, DeleteReportTypeCommandResponse>
        {
            private readonly IReportTypeRepository reportTypeRepository;
            private readonly IMapper mapper;
            private readonly ReportTypeBusinessRules reportTypeBusinessRules;
            public DeleteReportTypeCommandHandler(IReportTypeRepository reportTypeRepository, IMapper mapper, ReportTypeBusinessRules reportTypeBusinessRules)
            {
                this.reportTypeRepository = reportTypeRepository;
                this.mapper = mapper;
                this.reportTypeBusinessRules = reportTypeBusinessRules;
            }

            public async Task<DeleteReportTypeCommandResponse> Handle(DeleteReportTypeCommand request, CancellationToken cancellationToken)
            {
                var report = await reportTypeBusinessRules.ReportTypeCheckById(request.Id);

                var deletedReport = await reportTypeRepository.DeleteAsync(report);

                DeleteReportTypeCommandResponse response = mapper.Map<DeleteReportTypeCommandResponse>(deletedReport);

                return response;

            }
        }
    }
}
