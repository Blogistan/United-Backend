using FluentValidation;

namespace Application.Features.Reports.Queries.GetListReportDynamic
{
    public class GetListReportDynamicQueryValidator : AbstractValidator<GetListReportDynamicQuery>
    {
        public GetListReportDynamicQueryValidator()
        {
            RuleFor(x => x.DynamicQuery).NotNull();
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
