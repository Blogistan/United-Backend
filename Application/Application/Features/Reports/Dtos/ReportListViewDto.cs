using Application.Features.ReportTypes.Dtos;

namespace Application.Features.Reports.Dtos
{
    public record ReportListViewDto
    {
        public int Id { get; set; }
        public ReportUserListViewDto User { get; set; }
        public ReportTypeListViewDto ReportType { get; set; } 
        public string ReportDescription { get; set; } = string.Empty;
    }
    public record ReportUserListViewDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
