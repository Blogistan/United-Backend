using FluentValidation;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaimDynamic
{
    public class GetListOperationClaimDynamicQueryValidator : AbstractValidator<GetListOperationClaimDynamicQuery>
    {
        public GetListOperationClaimDynamicQueryValidator()
        {
            RuleFor(x=>x.DynamicQuery).NotEmpty();
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
