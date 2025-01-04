using FluentValidation;

namespace Application.Features.Contents.Queries.GetById
{
    public class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
    {
        public GetContentByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
