using FluentValidation;

namespace Application.Features.ReportTypes.Queries.GetListReportTypes
{
    public class GetListReportTypeQueryValidator : AbstractValidator<GetListReportTypeQuery>
    {
        public GetListReportTypeQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
