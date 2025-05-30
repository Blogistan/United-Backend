﻿using Application.Features.Blogs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommand : IRequest<CreateBlogCommandResponse>, ISecuredRequest
    {
        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int ContentId { get; set; }
        public string BannerImageUrl { get; set; } = string.Empty;
        public int WriterId { get; set; }
        public int ReactionSuprisedCount => 0;
        public int ReactionLovelyCount => 0;
        public int ReactionSadCount => 0;
        public int ReactionKEKWCount => 0;
        public int ReactionTriggeredCount => 0;

        public int ShareCount => 0;
        public int ReadCount => 0;

        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "User" };



        public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogCommandResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            private readonly BlogBusinessRules blogBusinessRules;
            private readonly ISiteUserRepository siteUserRepository;
            public CreateBlogCommandHandler(IBlogRepository blogRepository, IMapper mapper, BlogBusinessRules blogBusinessRules, ISiteUserRepository siteUserRepository)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
                this.blogBusinessRules = blogBusinessRules;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<CreateBlogCommandResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                await blogBusinessRules.BlogCannotBeDuplicatedWhenInserted(request.Title);

                Blog blog = mapper.Map<Blog>(request);
                var siteUser = await siteUserRepository.GetAsync(x => x.UserId == request.WriterId);
                blog.WriterId = siteUser!.Id;
                Blog createdBlog = await blogRepository.AddAsync(blog);

                Blog blogReturn = await blogRepository.GetAsync(x => x.Id == createdBlog.Id, include: inc => inc.Include(x => x.Writer).Include(x => x.Category));

                CreateBlogCommandResponse response = mapper.Map<CreateBlogCommandResponse>(blogReturn);
                return response;
            }
        }
    }
}
