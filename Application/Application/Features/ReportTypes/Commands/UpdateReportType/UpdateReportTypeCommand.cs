using Application.Features.ReportTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReportTypes.Commands.UpdateReportType
{
    public class UpdateReportTypeCommand:IRequest<UpdateReportTypeCommandResponse>
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;

        public class UpdateReportCommandHandler:IRequestHandler<UpdateReportTypeCommand, UpdateReportTypeCommandResponse>
        {
            private readonly IReportTypeRepository reportTypeRepository;
            private readonly IMapper mapper;
            private readonly ReportTypeBusinessRules reportTypeBusinessRules;
            public UpdateReportCommandHandler(IReportTypeRepository reportTypeRepository, IMapper mapper, ReportTypeBusinessRules reportTypeBusinessRules)
            {
                this.reportTypeRepository = reportTypeRepository;
                this.mapper = mapper;
                this.reportTypeBusinessRules = reportTypeBusinessRules;
            }

            public async Task<UpdateReportTypeCommandResponse> Handle(UpdateReportTypeCommand request, CancellationToken cancellationToken)
            {
               await reportTypeBusinessRules.ReportTypeCannotBeDuplicatedWhenUpdated(request.ReportTypeName);

                var report = await reportTypeRepository.GetAsync(x => x.Id == request.Id);


                report.ReportTypeDescription = request.ReportTypeDescription;
                report.ReportTypeName = request.ReportTypeName;

                var updatedReport = await reportTypeRepository.UpdateAsync(report);

                UpdateReportTypeCommandResponse response = mapper.Map<UpdateReportTypeCommandResponse>(updatedReport);

                return response;

               
            }
        }
    }
}
