using FluentValidation;

namespace Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic
{
    public class GetListUserOperationClaimDynamicQueryValidator:AbstractValidator<GetListUserOperationClaimDynamicQuery>
    {
        public GetListUserOperationClaimDynamicQueryValidator()
        {
            RuleFor(x=>x.PageRequest).NotEmpty();
            RuleFor(x=>x.DynamicQuery).NotEmpty();

        }
    }
}
