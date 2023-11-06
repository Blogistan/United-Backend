using FluentValidation;

namespace Application.Features.ReportTypes.Commands.DeleteReportType
{
    public class DeleteReportTypeCommandValidator:AbstractValidator<DeleteReportTypeCommand>
    {
        public DeleteReportTypeCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
