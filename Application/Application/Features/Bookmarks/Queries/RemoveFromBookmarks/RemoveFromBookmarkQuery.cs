using Application.Features.Auth.Rules;
using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookmarks.Queries.RemoveFromBookmarks
{
    public class RemoveFromBookmarkQuery : IRequest<bool>,ISecuredRequest
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

        public class RemoveFromBookmarkQueryHandler : IRequestHandler<RemoveFromBookmarkQuery, bool>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly BlogBusinessRules blogBusinessRules;
            public RemoveFromBookmarkQueryHandler(BlogBusinessRules blogBusinessRules, AuthBussinessRules authBussinessRules, ISiteUserRepository siteUserRepository)
            {
                this.blogBusinessRules = blogBusinessRules;
                this.authBussinessRules = authBussinessRules;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<bool> Handle(RemoveFromBookmarkQuery request, CancellationToken cancellationToken)
            {
                await blogBusinessRules.BlogCheckById(request.BlogId);
                SiteUser user = await siteUserRepository.GetAsync(x => x.Id == request.UserId, x => x.Include(x => x.Bookmarks));

                await authBussinessRules.UserShouldBeExist(user);


                var blogToBeRemove = user.Bookmarks.Single(x => x.BlogId == request.BlogId);
                user.Bookmarks.Remove(blogToBeRemove);
                await siteUserRepository.UpdateAsync(user);

                return true;
            }
        }
    }
}
