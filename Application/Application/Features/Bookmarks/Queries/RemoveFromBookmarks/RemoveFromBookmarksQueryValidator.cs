﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bookmarks.Queries.RemoveFromBookmarks
{
    public class RemoveFromBookmarksQueryValidator:AbstractValidator<RemoveFromBookmarkQuery>
    {
        public RemoveFromBookmarksQueryValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty();
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
    
}
