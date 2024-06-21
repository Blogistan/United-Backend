﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Contents.Rules
{
    public class ContentBusinessRules
    {
        public readonly IContentRepository contentRepository;
        public ContentBusinessRules(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public async Task<Content> ContentCheckById(int contentId)
        {
            Content content = await contentRepository.GetAsync(x => x.Id == contentId);
            if (content is null)
                throw new BusinessException("Content is not exist");

            return content;
        }
    }
}
