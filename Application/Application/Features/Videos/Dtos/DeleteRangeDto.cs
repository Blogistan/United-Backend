namespace Application.Features.Videos.Dtos
{
    public class DeleteRangeDto
    {
        public int VideoIds { get; set; }
        public bool Permanent { get; set; }
    }
}
