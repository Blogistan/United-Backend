using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blogs.Queries.KEKWBlog
{
    public class KekwBlogQueryValidator:AbstractValidator<KekwBlogQuery>
    {
        public KekwBlogQueryValidator()
        {
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
