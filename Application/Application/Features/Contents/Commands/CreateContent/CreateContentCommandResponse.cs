using Core.Application.Responses;

namespace Application.Features.Contents.Commands.CreateContent
{
    public class CreateContentCommandResponse : IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? ContentImageUrl { get; set; } = string.Empty;

        public string ContentPragraph { get; set; } = string.Empty;

        public CreateContentCommandResponse(int id, string title, string? contentImageUrl, string contentPragraph)
        {
            this.Id = id;
            Title = title;
            ContentImageUrl = contentImageUrl;
            ContentPragraph = contentPragraph;
        }
    }
}
