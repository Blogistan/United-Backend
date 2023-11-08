using FluentValidation;

namespace Application.Features.ReportTypes.Queries.GetListReportTypes
{
    public class GetListReportTypeQueryResponseValidator:AbstractValidator<GetListReportTypeQuery>
    {
        public GetListReportTypeQueryResponseValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
        }
    }
}
