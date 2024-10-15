namespace Application.Features.Reports.Dtos
{
    public record ReportListViewDto
    {
        public int Id { get; set; }
        public ReportUserListViewDto User { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string ReportDescription { get; set; } = string.Empty;
    }
    public record ReportUserListViewDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
