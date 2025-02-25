﻿using Core.Application.Responses;

namespace Application.Features.Contents.Commands.DeleteContent
{
    public record DeleteContentCommandResponse :IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? ContentImageUrl { get; set; } = string.Empty;

        public string ContentPragraph { get; set; } = string.Empty;

        public DeleteContentCommandResponse()
        {
            
        }
        public DeleteContentCommandResponse(int id, string title, string? contentImageUrl, string contentPragraph)
        {
            Id = id;
            Title = title;
            ContentImageUrl = contentImageUrl;
            ContentPragraph = contentPragraph;
        }
    }
}
