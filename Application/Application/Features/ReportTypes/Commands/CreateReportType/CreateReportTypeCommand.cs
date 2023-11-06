using Application.Features.ReportTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReportTypes.Commands.CreateReportType
{
    public class CreateReportTypeCommand : IRequest<CreateReportTypeCommandResponse>
    {
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;


        public class CreateReportTypeCommandHandler : IRequestHandler<CreateReportTypeCommand, CreateReportTypeCommandResponse>
        {
            private readonly IReportTypeRepository reportTypeRepository;
            private readonly IMapper mapper;
            private readonly ReportTypeBusinessRules reportTypeBusinessRules;
            public CreateReportTypeCommandHandler(IReportTypeRepository reportTypeRepository, IMapper mapper, ReportTypeBusinessRules reportType)
            {
                this.reportTypeRepository = reportTypeRepository;
                this.mapper = mapper;
                this.reportTypeBusinessRules = reportType;
            }

            public async Task<CreateReportTypeCommandResponse> Handle(CreateReportTypeCommand request, CancellationToken cancellationToken)
            {
                await reportTypeBusinessRules.ReportTypeCannotBeDuplicatedWhenInserted(request.ReportTypeName);

                ReportType reportType = mapper.Map<ReportType>(request);

                var createdReportType = await reportTypeRepository.AddAsync(reportType);

                var response = mapper.Map<CreateReportTypeCommandResponse>(createdReportType);

                return response;




            }
        }
    }
}
