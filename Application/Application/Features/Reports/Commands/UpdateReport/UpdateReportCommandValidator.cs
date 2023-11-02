using FluentValidation;

namespace Application.Features.Reports.Commands.UpdateReport
{
    public class UpdateReportCommandValidator:AbstractValidator<UpdateReportCommand>
    {
        public UpdateReportCommandValidator()
        {
            RuleFor(x=>x.ReportID).NotEmpty();
        }
    }
}
