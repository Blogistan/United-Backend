using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.DeleteRangeCategory
{
    public class DeleteRangeCategoryValidator:AbstractValidator<DeleteRangeCategoryCommand>
    {
        public DeleteRangeCategoryValidator()
        {
            RuleFor(x=>x.Permanent).NotEmpty();
            RuleFor(x=>x.DeleteRangeCategoryDtos).NotEmpty();
        }
    }
}
