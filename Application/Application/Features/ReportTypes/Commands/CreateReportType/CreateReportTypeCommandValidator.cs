using FluentValidation;

namespace Application.Features.ReportTypes.Commands.CreateReportType
{
    public class CreateReportTypeCommandValidator:AbstractValidator<CreateReportTypeCommand>
    {
        public CreateReportTypeCommandValidator()
        {
            RuleFor(x=>x.ReportTypeName).NotEmpty();
            RuleFor(x=>x.ReportTypeDescription).NotEmpty();
        }
    }
}
