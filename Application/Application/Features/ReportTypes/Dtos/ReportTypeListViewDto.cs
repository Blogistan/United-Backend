namespace Application.Features.ReportTypes.Dtos
{
    public class ReportTypeListViewDto
    {
        public int Id { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public string ReportTypeDescription { get; set; } = string.Empty;
    }
}
