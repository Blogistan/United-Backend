using Application.Features.Auth.Rules;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookmarks.Queries.AddToBookmarks
{
    public class AddToBookmarksQuery : IRequest<bool>,ISecuredRequest
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User"};

        public class AddToBookmarksQueryHandler : IRequestHandler<AddToBookmarksQuery, bool>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly BlogBusinessRules blogBusinessRules;
            public AddToBookmarksQueryHandler(ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, BlogBusinessRules blogBusinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.blogBusinessRules = blogBusinessRules;
            }
            public async Task<bool> Handle(AddToBookmarksQuery request, CancellationToken cancellationToken)
            {
                await blogBusinessRules.BlogCheckById(request.BlogId);
                SiteUser user = await siteUserRepository.GetAsync(x => x.UserId == request.UserId, x => x.Include(x => x.Bookmarks).Include(x=>x.User));

                await authBussinessRules.UserShouldBeExist(user.User);

                Bookmark bookmark = new()
                {
                    BlogId = request.BlogId,
                    SiteUserId = user.Id,
                    SiteUser = user
                };
                user.Bookmarks.Add(bookmark);
                await siteUserRepository.UpdateAsync(user);

                return true;
            }
        }
    }
}
