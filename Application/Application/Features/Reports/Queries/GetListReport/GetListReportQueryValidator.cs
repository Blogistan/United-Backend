using FluentValidation;

namespace Application.Features.Reports.Queries.GetListReport
{
    public class GetListReportQueryValidator:AbstractValidator<GetListReportQuery>
    {
        public GetListReportQueryValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
