﻿using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookmarks.Queries.GetListBookmarks
{
    public class GetListBookmarksQuery : IRequest<GetListBookmarkQueryResponse>,ISecuredRequest
    {
        public int UserId { get; set; }

        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

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
                var user = await siteUserRepository.GetAsync(x => x.UserId == request.UserId, x => x.Include(x => x.Bookmarks).ThenInclude(x => x.Blog).ThenInclude(x => x.Category).Include(x => x.Bookmarks).ThenInclude(x => x.Blog).ThenInclude(x => x.Category)
                .Include(x => x.Bookmarks).ThenInclude(x => x.Blog).ThenInclude(x => x.Writer).Include(x=>x.User)
             );
                await authBussinessRules.UserShouldBeExist(user.User);

              
                var response = mapper.Map<GetListBookmarkQueryResponse>(user);

                return response;
            }
        }
    }
}
