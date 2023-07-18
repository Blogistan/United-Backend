using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetCommentResponsesQuery
{
    public class GetCommentResponsesQueryValidator:AbstractValidator<GetCommentResponsesQuery>
    {
        public GetCommentResponsesQueryValidator()
        {
            RuleFor(x=>x.CommentId).NotEmpty();
        }
    }
}
