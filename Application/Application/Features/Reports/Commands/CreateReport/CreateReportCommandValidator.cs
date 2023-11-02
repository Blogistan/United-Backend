using FluentValidation;

namespace Application.Features.Reports.Commands.CreateReport
{
    public class CreateReportCommandValidator:AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x=>x.UserID).NotEmpty();
            RuleFor(x=>x.ReportTypeID).NotEmpty();
        }
    }
}
