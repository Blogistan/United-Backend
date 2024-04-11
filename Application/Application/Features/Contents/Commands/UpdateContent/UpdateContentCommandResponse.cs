using Core.Application.Responses;

namespace Application.Features.Contents.Commands.UpdateContent
{
    public class UpdateContentCommandResponse : IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? ContentImageUrl { get; set; } = string.Empty;

        public string ContentPragraph { get; set; } = string.Empty;


        public UpdateContentCommandResponse()
        {
            
        }
        public UpdateContentCommandResponse(int id, string title, string? contentImageUrl, string contentPragraph)
        {
            Id = id;
            Title = title;
            ContentImageUrl = contentImageUrl;
            ContentPragraph = contentPragraph;
        }
    }
}
