namespace Infrastructure.OpenAI.Abstract
{
    public interface IAiService
    {
        Task<string> GenerateResponse(string prompt);
    }
}
