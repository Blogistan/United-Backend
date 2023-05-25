namespace Application.Features.Contents.Commands.DeleteContent
{
    public class DeleteContentCommandResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }
    }
}
