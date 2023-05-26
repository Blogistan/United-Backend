namespace Application.Features.Contents.Commands.UpdateContent
{
    public class UpdateContentCommandResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }
    }
}
