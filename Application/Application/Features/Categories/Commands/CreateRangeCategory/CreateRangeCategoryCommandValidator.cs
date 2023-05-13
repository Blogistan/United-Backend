using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.CreateRangeCategory
{
    public class CreateRangeCategoryCommandValidator:AbstractValidator<CreateRangeCategoryCommand>
    {
        public CreateRangeCategoryCommandValidator()
        {
            RuleFor(x=>x.CreateCategoryDtos).NotEmpty();
        }
    }
}
