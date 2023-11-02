using FluentValidation;

namespace Application.Features.Reports.Commands.DeleteReport
{
    public class DeleteReportCommandValidator:AbstractValidator<DeleteReportCommand>
    {
        public DeleteReportCommandValidator()
        {
            RuleFor(x=>x.ReportID).NotEmpty();
        }
    }
}
