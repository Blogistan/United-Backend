namespace Application.Features.OperationClaims.Dtos
{
    public record OperationClaimListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
