using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blogs.Queries.LikeBlog
{
    public class ReadBlogQueryValidator:AbstractValidator<ReadBlogQuery>
    {
        public ReadBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
