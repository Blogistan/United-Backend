using FluentValidation;

namespace Application.Features.ReportTypes.Commands.UpdateReportType
{
    public class UpdateReportTypeCommandValidator:AbstractValidator<UpdateReportTypeCommand>
    {
        public UpdateReportTypeCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.ReportTypeName).NotEmpty();
            RuleFor(x=>x.ReportTypeDescription).NotEmpty();
        }
    }
}
