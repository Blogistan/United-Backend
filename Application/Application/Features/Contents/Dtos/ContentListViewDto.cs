namespace Application.Features.Contents.Dtos
{
    public class ContentListViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? ContentImageUrl { get; set; } = string.Empty;

        public string ContentPragraph { get; set; } = string.Empty;
    }
}
