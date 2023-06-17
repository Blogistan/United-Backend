using Application.Features.Auth.Rules;
using Application.Features.Blogs.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookmarks.Queries.GetListBookmarks
{
    public class GetListBookmarksQuery : IRequest<GetListBookmarkQueryResponse>
    {
        public int UserId { get; set; }
        public class GetListBookmarksQueryHandler : IRequestHandler<GetListBookmarksQuery, GetListBookmarkQueryResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly AuthBussinessRules authBussinessRules;
            public GetListBookmarksQueryHandler(ISiteUserRepository userRepository, IMapper mapper, AuthBussinessRules authBussinessRules)
            {
                this.siteUserRepository = userRepository;
                this.mapper = mapper;
                this.authBussinessRules = authBussinessRules;
            }

            public async Task<GetListBookmarkQueryResponse> Handle(GetListBookmarksQuery request, CancellationToken cancellationToken)
            {
                var user = await siteUserRepository.GetAsync(x => x.Id == request.UserId, x => x.Include(x => x.Bookmarks).ThenInclude(x=>x.Blog).ThenInclude(x => x.Category).Include(x => x.Bookmarks).ThenInclude(x => x.Blog).ThenInclude(x=>x.Category)
                .Include(x => x.Bookmarks).ThenInclude(x => x.Blog).ThenInclude(x => x.Writer)
             );
                await authBussinessRules.UserShouldBeExist(user);

                GetListBookmarkQueryResponse response = new();
                List<BlogListViewDto> blogListViewDtos = new();
                user.Bookmarks.ToList().ForEach(x => blogListViewDtos.Add(

                     new BlogListViewDto
                     {
                         Id = x.Blog.Id,
                         CategoryName = x.Blog.Category.CategoryName,
                         BannerImageUrl = x.Blog.BannerImageUrl,
                         ReactionKEKWCount = x.Blog.ReactionKEKWCount,
                         ReactionLovelyCount = x.Blog.ReactionLovelyCount,
                         ReactionSadCount = x.Blog.ReactionSadCount,
                         ReactionSuprisedCount = x.Blog.ReactionSuprisedCount,
                         ReactionTriggeredCount = x.Blog.ReactionTriggeredCount,
                         Title = x.Blog.Title,
                         ShareCount = x.Blog.ShareCount,
                         WriterName = x.Blog.Writer.FirstName + " " + x.Blog.Writer.LastName,
                     }
                    ));

                response.Items = blogListViewDtos;

                return response;
            }
        }
    }
}
