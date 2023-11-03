namespace Application.Features.Reports.Dtos
{
    public class ReportListViewDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
    }
}
