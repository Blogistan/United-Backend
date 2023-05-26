namespace Application.Features.Contents.Dtos
{
    public class ContentListViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string? ContentImageUrl { get; set; }

        public string ContentPragraph { get; set; }
    }
}
